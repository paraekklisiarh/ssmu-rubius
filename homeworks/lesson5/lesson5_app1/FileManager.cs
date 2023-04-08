namespace lesson5_app1;

public static class FileManager
{
    public static List<DirectoryItem> GetDirectoryContents(string dirName)
    {
        var content = new List<DirectoryItem>();

        var directories = Directory.GetDirectories(dirName);
        // Написал двумя вариантами для напоминания себе в будущем.
        foreach (var directory in directories)
        {
            content.Add(new DirectoryItem(
                DirectoryItem.ItemTypes.Directory,
                directory.Remove(0,dirName.Length+1), //archive\dir -> dir
                Directory.GetLastWriteTime(dirName)));
        }

        var files = Directory.GetFiles(dirName);
        content.AddRange(files.Select(
            file => new DirectoryItem(
                Type: DirectoryItem.ItemTypes.File,
                Name: file.Remove(0,dirName.Length+1), // archive/file -> file
                DateLastChange: File.GetLastWriteTime(file))));

        return content;
    }

    public static void ForceRecordToNewFile(string filePath, string content)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine($"Удален существующий файл {filePath}");
        }

        using var writer = new StreamWriter(filePath);
        writer.Write(content);
        // Так можно делать только если всегда ожидаем микроскопическую запись, а мы ожидаем.
        Console.WriteLine($"\"{content}\" записано в {filePath}");
    }
}