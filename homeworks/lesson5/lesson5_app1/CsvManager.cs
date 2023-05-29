namespace lesson5_app1;

public static class CsvManager
{
    /// <summary>
    /// Метод создает CSV-таблицу из списка объектов класса <see cref="DirectoryItem"/>
    /// </summary>
    /// <param name="directoryItems"></param>
    /// <param name="fileName"></param>
    /// <param name="separator"></param>
    public static async Task RecordDirectoryContent(List<DirectoryItem> directoryItems, string fileName, string separator = ",")
    {
        await using var writer = new StreamWriter(fileName);
        foreach (var directoryItem in directoryItems)
        {
            await writer.WriteLineAsync(
                $"\"{directoryItem.Type}\"{separator}\"{directoryItem.Name}\"{separator}\"{directoryItem.DateLastChange}\"");
        }
    }
}