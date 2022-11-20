using MediatR;
using Domain.Counter;
using Microsoft.Extensions.Options;
using Domain.ConfigurationSettings;
using Domain.Common.Database.Abstractions;
using Domain.Common.FileHalper.Abstraction;

namespace Application.Commands.WriteInDatabaseFromCsv;

public class WriteInDatabaseFromCsvCommandHandler : IRequestHandler<WriteInDatabaseFromCsvCommand>
{
    private readonly IFileProvider _fileProvider;
    
    private List<HouseholdMeteringPlant> _entities = new();
    
    private readonly IOptions<CsvReaderOptions> _csvReaderOptions;
    
    private readonly IRepository<HouseholdMeteringPlant> _householdMeteringPlantRepository;

    public WriteInDatabaseFromCsvCommandHandler(IRepository<HouseholdMeteringPlant> householdMeteringPlantRepository
        , IOptions<CsvReaderOptions> csvReaderOptions
        , IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
        _csvReaderOptions = csvReaderOptions;
        _householdMeteringPlantRepository = householdMeteringPlantRepository;
    }

    public async Task<Unit> Handle(WriteInDatabaseFromCsvCommand request, CancellationToken cancellationToken)
    {
        foreach (var path in request.CsvPath.Paths)
        {
            if (!_fileProvider.Exists(path))
            {
                continue;
            }
            using (var reader = _fileProvider.GetStreamReader(path))
            {
                var headRow = GetHeadRow(reader);
                var index = Array.IndexOf(headRow.Keys.ToArray(), _csvReaderOptions.Value.ColumnToFilter);

                while (!reader.EndOfStream)
                {
                    var words = GetFilteredLineInWords(index, reader);
                    if (words.Length == decimal.Zero)
                    {
                        continue;
                    }

                    var entity = GetHouseholdMeteringPlant(headRow, words);
                    // grouping by TINKLAS
                    if (!_entities.Any(o => o.TINKLAS == entity?.TINKLAS))
                    {
                        _entities.Add(entity);
                    }
                }
            }
            _fileProvider.Delete(path);
        }

        await _householdMeteringPlantRepository.AddRangeAsync(_entities, cancellationToken);
        await _householdMeteringPlantRepository.SaveChangesAsync(cancellationToken);

        return new();
    }

    private string[] GetFilteredLineInWords(int index, StreamReader reader)
    {
        var words = reader.ReadLine().Split(_csvReaderOptions.Value.Separator);
        if (index != -1 && words.Length >= index && words[index].ToLower() != _csvReaderOptions.Value.ValueToFilter)
        {
            return new string[] { };
        }

        return words;
    }

    private static HouseholdMeteringPlant GetHouseholdMeteringPlant(Dictionary<string, string> headRows, string[] words)
    {
        int counter = 0;
        foreach (var key in headRows.Keys)
        {
            headRows[key] = words[counter++];
        }

        var row = ToHouseholdMeteringPlant(headRows);

        return row;
    }

    private static HouseholdMeteringPlant ToHouseholdMeteringPlant(Dictionary<string, string> headRows)
    {
        var result = new HouseholdMeteringPlant();
        foreach (var headRow in headRows)
        {
            switch (headRow.Key)
            {
                case "PPlus":
                    _ = decimal.TryParse(headRow.Value, out decimal pPlus) ? result.PPlus = pPlus : result.PPlus = null;
                    break;

                case "PMinus":
                    _ = decimal.TryParse(headRow.Value, out decimal pMinus) ? result.PMinus = pMinus : result.PMinus = null;
                    break;

                case "OBJ_NUMERIS":
                    _ = int.TryParse(headRow.Value, out int obj_numeris);
                    result.OBJ_NUMERIS = obj_numeris;
                    break;

                case "PL_T":
                    _ = DateTime.TryParse(headRow.Value, out DateTime pl_t);
                    result.PL_T = pl_t;
                    break;

                case "TINKLAS":
                    result.TINKLAS = headRow.Value;
                    break;

                case "OBJ_GV_TIPAS":
                    result.OBJ_GV_TIPAS = headRow.Value;
                    break;

                case "OBT_PAVADINIMAS":
                    result.OBT_PAVADINIMAS = headRow.Value;
                    break;
            }
        }

        return result;
    }

    private Dictionary<string, string> GetHeadRow(StreamReader reader)
    {
        if (reader.EndOfStream)
        {
            return new();
        }

        return reader
                .ReadLine()
                .Split(_csvReaderOptions.Value.Separator)
                .GroupBy(propertyName => propertyName)
                .ToDictionary(propertyName => GetName(propertyName.Key), word => string.Empty);

        string GetName(string key) => key switch
        {
            "P-" => "PMinus",
            "P+" => "PPlus",
            _ => key
        };
    }
}