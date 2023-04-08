// Первая из программ совершает следующие действия:
// 1. Распаковывает архив в папку рядом с запускаемым файлом программы
// 2. Считывает названия файлов и папок из указанной папки
// 3. Записывает информацию о содержимом папки (тип (файл/папка), название, дата
// изменения) в текстовый файл в формате .csv (разделитель – \t (знак табуляции) )
// 4. Удаляет папку с распакованным архивом
// 5. Сохраняет путь к файлу csv в файле %AppData%/Lesson12Homework.txt

using System.IO.Compression;

namespace lesson5_app1;

static class Lesson5App1
{
    const string ArchiveName = "archive.zip";
    const string UnarchDirectory = "archive";
    const string CsvName = "DirectoryItemsList.csv";

    const string Separator = @"\t";

    // Жесть, что за названия!
    const string PathToTemporaryFile = @"%AppData%\Lesson12Homework.txt";

    static void Unzip(string archiveName, string unarchDirectory)
    {
        if (Directory.Exists(unarchDirectory))
        {
            Directory.Delete(unarchDirectory, true);
        }

        ZipFile.ExtractToDirectory(sourceArchiveFileName: archiveName,
            destinationDirectoryName: unarchDirectory);
        Console.WriteLine($"Распакован архив {archiveName}");
    }

    static void Main()
    {
        // 1. Распаковывает архив в папку рядом с запускаемым файлом программы
        try
        {
            Unzip(ArchiveName, UnarchDirectory);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Архива {ArchiveName} в директории программы не существует.");
            throw;
        }
        catch (InvalidDataException)
        {
            Console.WriteLine($"Файл {ArchiveName} не является архивом или повреждён.");
            throw;
        }
        // Не уверен, что воспроизводится в винде, но мало ли чо.
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Доступ к файлу {ArchiveName} запрещён. Измените атрибуты файла.");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        // 2. Считывает названия файлов и папок из указанной папки
        List<DirectoryItem> directoryContent;
        
        try
        {
            directoryContent = FileManager.GetDirectoryContents(UnarchDirectory);
        }
        // Вызываю прерывания потому, что получить названия критически важно.
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Директория {Path.GetFullPath(UnarchDirectory)} была удалена");
            throw;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Доступ к директории {Path.GetFullPath(UnarchDirectory)} запрещён.");
            throw;
        }

        if (directoryContent.Count == 0)
        {
            throw new Exception("Исследуемый архив пуст.");
        }

        // 3. Записывает информацию о содержимом папки (тип (файл/папка), название, дата
        // изменения) в текстовый файл в формате .csv (разделитель – \t (знак табуляции) )
        try
        {
            CsvManager.RecordDirectoryContent(
                directoryItems: directoryContent,
                fileName: CsvName,
                separator: Separator);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Файл {CsvName} защищен от записи. Измените его атрибуты.");
            throw;
        }
        catch (IOException)
        {
            Console.WriteLine($"Файл {CsvName} открыт в другом приложении.");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        // 4. Удаляет папку с распакованным архивом
        try
        {
            Directory.Delete(UnarchDirectory, true);
        }
        // Не вызываю прерывания потому, что на итог программы не повлияет - файлы уже прочитаны.
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Директория {UnarchDirectory} уже не существует.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Директория {UnarchDirectory} защищена от изменений.");
        }
        catch (Exception e)
        {
            // IOException объединяет слишком много ожидаемых ошибок.
            // Я не отделю "только для чтения" от "используется другим".
            Console.WriteLine(e);
        }

        // 5. Сохраняет путь к файлу csv в файле %AppData%/Lesson12Homework.txt
        try
        {
            FileManager.ForceRecordToNewFile(
                filePath: Environment.ExpandEnvironmentVariables(PathToTemporaryFile),
                content: Path.GetFullPath(CsvName));
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Доступ к файлу {PathToTemporaryFile} запрещён.");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}