// Вторая программа:
// 1. Считывает путь к файлу из %AppData%/Lesson12Homework.txt
// 2. Открывает указанный файл .csv
// 3. Выводит в консоль список файлов, прочитанный из файла csv, отсортированный по дате изменения
// 4. Удаляет файл %AppData%/Lesson12Homework.txt

/*
 * Кривые моменты подхода:
 * 1. Что File.ReadAllLines(), что StreamReader.ReadLine() ведут себя строго как задокументированно
 * * и радостно рвут строку на \r, \n и \r\n. То есть от дополнительных разрывов строки нас не спасёт ничто.
 * * Для CSV есть специально обученные библиотеки, в нормальном коде следует использовать их.
 * * Либо писать толстый обработчик. Meh.
 * 2. Плохо (специально, да?) выбран сепаратор в CSV.
 * * \t в виндовских путях будет встречаться повсеместно и портить всю малину.
 */

namespace lesson5_app2;

static class Lesson5App2
{
    private static readonly string PathToDesiredFile =
        Environment.ExpandEnvironmentVariables(@"%AppData%/Lesson12Homework.txt");

    static void Main()
    {
        // 1. Считывает путь к файлу из %AppData%/Lesson12Homework.txt

        string pathToCsvFile;
        try
        {
            pathToCsvFile = File.ReadAllText(PathToDesiredFile);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {PathToDesiredFile} не существует.");
            Console.WriteLine("Запустите программу №1.");
            throw;
        }
        catch (FileLoadException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (pathToCsvFile.Length == 0)
        {
            Console.WriteLine($"Файл {PathToDesiredFile} пуст или повреждён. Запустите программу №1.");
            return;
        }

        // 2. Открывает указанный файл .csv
        List<DirectoryItem> contentList;
        try
        {
            contentList = CsvManager.GetItemsFromCsv(pathToCsvFile);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        // 3. Выводит в консоль список файлов, прочитанный из файла csv, отсортированный по дате изменения
        Console.WriteLine($"{"Тип",2} {"Название",20} {"Дата изменения",40}");
        foreach (var item in contentList.OrderBy(x => x.DateLastChange))
        {
            Console.WriteLine($"{item.Type,2} {item.Name,4} {item.DateLastChange,40}");
        }

        // 4. Удаляет файл %AppData%/Lesson12Homework.txt
        File.Delete(PathToDesiredFile);
    }
}