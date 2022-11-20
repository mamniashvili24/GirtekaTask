namespace Domain.ConfigurationSettings;

public class CsvReaderOptions
{
    public string Separator { get; set; }

    public string ValueToFilter { get; set; }

    public string ColumnToFilter { get; set; }
}