namespace lesson5_app1;

public static class CsvManager
{
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