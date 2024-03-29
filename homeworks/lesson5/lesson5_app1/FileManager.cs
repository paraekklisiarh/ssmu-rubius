﻿namespace lesson5_app1;

public static class FileManager
{
    /// <summary>
    /// Метод, возвращающий содержимое указанной папки
    /// </summary>
    /// <param name="dirPath"></param>
    /// <returns>Список из объектов класса <see cref="DirectoryItem"/></returns>
    public static List<DirectoryItem> GetDirectoryContent(string dirPath)
    {
        var content = new List<DirectoryItem>();

        var directories = Directory.GetDirectories(dirPath);
        // Написал двумя вариантами для напоминания себе в будущем.
        foreach (var directory in directories)
        {
            content.Add(new DirectoryItem(
                DirectoryItem.ItemTypes.Directory,
                directory.Remove(0,dirPath.Length+1), //archive\dir -> dir
                Directory.GetLastWriteTime(directory)));
        }

        var files = Directory.GetFiles(dirPath);
        content.AddRange(files.Select(
            file => new DirectoryItem(
                Type: DirectoryItem.ItemTypes.File,
                Name: file.Remove(0,dirPath.Length+1), // archive/file -> file
                DateLastChange: File.GetLastWriteTime(file))));

        return content;
    }

    /// <summary>
    /// Метод создает файл и записывает в него строку.
    /// Существующий файл перезаписывается.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="content"></param>
    public static async Task ForceRecordToNewFileAsync(string filePath, string content)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine($"Удален существующий файл {filePath}");
        }

        await using var writer = new StreamWriter(filePath);
        await writer.WriteAsync(content);
        
        // Так можно делать только если всегда ожидаем микроскопическую запись, а мы ожидаем.
        Console.WriteLine($"\"{content}\" записано в {filePath}");
    }
}