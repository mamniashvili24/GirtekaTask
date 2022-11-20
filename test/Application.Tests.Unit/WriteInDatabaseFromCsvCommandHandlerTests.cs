using Xunit;
using NSubstitute;
using System.Text;
using Domain.Counter;
using Domain.ConfigurationSettings;
using Microsoft.Extensions.Options;
using Domain.Common.Database.Abstractions;
using Domain.Common.FileHalper.Abstraction;
using Application.Commands.WriteInDatabaseFromCsv;

namespace Application.Tests.Unit;

public class WriteInDatabaseFromCsvCommandHandlerTests
{
    private readonly WriteInDatabaseFromCsvCommandHandler _sut;

	private readonly IFileProvider _fileProvider = Substitute.For<IFileProvider>();
	
    private readonly IOptions<CsvReaderOptions> _csvReaderOptions = Substitute.For<IOptions<CsvReaderOptions>>();
	
    private readonly IRepository<HouseholdMeteringPlant> _householdMeteringPlantRepository = Substitute.For<IRepository<HouseholdMeteringPlant>>();
    
    public WriteInDatabaseFromCsvCommandHandlerTests()
	{
		_sut = new(_householdMeteringPlantRepository, _csvReaderOptions, _fileProvider);
	}

	[Fact]
    public async Task Handle_ParsAndWriteInDb_WhenFilesNotExist()
	{
        // Arrange
        var filePaths = new string[] { "2022.12.12.csv", "2022.11.11.csv" };
        _fileProvider.Exists(Arg.Any<string>()).Returns(false);

        // Act
        await _sut.Handle(new WriteInDatabaseFromCsvCommand(filePaths), default);

        // Assert
        _fileProvider.Received(filePaths.Length).Exists(Arg.Any<string>());
    }

    [Fact]
    public async Task Handle_ParsAndWriteInDb_WhenOneFileExistAndItEmpty()
    {
        // Arrange
        var filePaths = new string[] { "2022.12.12.csv", "2022.11.11.csv" };
        _fileProvider.Exists(Arg.Is(filePaths[0])).Returns(true);
        _csvReaderOptions.Value.Returns(new CsvReaderOptions
        {
            Separator = ",",
            ValueToFilter = "butas",
            ColumnToFilter = "OBT_PAVADINIMAS"
        });

        _fileProvider.GetStreamReader(Arg.Is(filePaths[0])).Returns(
            new StreamReader(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(string.Empty)
                    )
                )
            );
        
        // Act
         await _sut.Handle(new WriteInDatabaseFromCsvCommand(filePaths), default);

        // Assert
        _fileProvider.Received(1).Delete(Arg.Any<string>());
        _fileProvider.Received(filePaths.Length).Exists(Arg.Any<string>());
        _householdMeteringPlantRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        _householdMeteringPlantRepository.Received(1).AddRangeAsync(Arg.Any<List<HouseholdMeteringPlant>>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ParsAndWriteInDb_WhenOneFileExistAndFilterdData()
    {
        // Arrange
        var filePath = "2022.12.12.csv";
        _fileProvider.Exists(Arg.Is(filePath)).Returns(true);
        _csvReaderOptions.Value.Returns(new CsvReaderOptions
        {
            Separator = ",",
            ValueToFilter = "butas",
            ColumnToFilter = "OBT_PAVADINIMAS"
        });

        _fileProvider.GetStreamReader(Arg.Is(filePath)).Returns(
                new StreamReader(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes($@"TINKLAS,OBT_PAVADINIMAS,OBJ_GV_TIPAS,OBJ_NUMERIS,P+,PL_T,P-{Environment.NewLine}Klaipėdos regiono tinklas,Butas,,4873840,1.3193,2022-05-31 00:00:00,0.0")
                        )
                    )
                );

        // Act
        await _sut.Handle(new WriteInDatabaseFromCsvCommand(new string[] { filePath }), default);

        // Assert
        _fileProvider.Received(1).Delete(Arg.Any<string>());
        _fileProvider.Received(1).Exists(Arg.Any<string>());
        _householdMeteringPlantRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        _householdMeteringPlantRepository.Received(1).AddRangeAsync(Arg.Any<List<HouseholdMeteringPlant>>(), Arg.Any<CancellationToken>());
    }
}