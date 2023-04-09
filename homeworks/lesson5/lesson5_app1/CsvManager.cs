namespace lesson5_app1;

public static class CsvManager
{
    /// <summary>
    /// Метод создает CSV-таблицу из списка объектов класса directoryItem
    /// </summary>
    /// <param name="directoryItems"></param>
    /// <param name="fileName"></param>
    /// <param name="separator"></param>
    public static void RecordDirectoryContent(List<DirectoryItem> directoryItems, string fileName, string separator)
    {
        using var writer = new StreamWriter(fileName);
        foreach (var directoryItem in directoryItems)
        {
            writer.WriteLine(
                $"\"{directoryItem.Type}\"{separator}\"{directoryItem.Name}\"{separator}\"{directoryItem.DateLastChange}\"");
        }
    }
}