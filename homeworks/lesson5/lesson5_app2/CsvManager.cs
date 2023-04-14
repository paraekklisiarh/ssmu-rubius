namespace lesson5_app2;

public static class CsvManager
{
    private const string Separator = "\"\\t\"";

    private static async Task<string> ReadCsv(string pathToCsv)
    {
        using var streamReader = new StreamReader(pathToCsv);
        var rawCsv = await streamReader.ReadToEndAsync();
        if (rawCsv == null)
        {
            throw new FileLoadException();
        }
        return rawCsv;
    }

    private static List<string> ParseLines(string? rawCsv)
    {
        var csv = new List<string>();
        
        csv.AddRange(rawCsv!.Split("\r\n"));
        csv.Remove(csv.Last()); // Костыль: удаляем пустую строку (\r\n в конце последней строки CSV)
        
        return csv;
    }

    /// <summary>
    /// Метод, преобразующий CSV в объекты класса DirectoryItem
    /// </summary>
    /// <param name="pathToCsv"></param>
    /// <returns>Список из объектов класса <see cref="DirectoryItem" /> </returns>
    /// <exception cref="FileLoadException"></exception>
    public static async Task<List<DirectoryItem>> GetItemsFromCsv(string pathToCsv)

    {
        var csv = ParseLines(await ReadCsv(pathToCsv));

        var items = new List<DirectoryItem>();

        // Я знаю, что это можно свернуть через LINQ,
        // но я этого не делаю потому, что не до конца в нём преисполнился.
        foreach (var line in csv)
        {
            var tempStr = line.Split(Separator);
            if (tempStr.Length != 3) // Магическое число. А потому что надо использовать нормальные библиотеки!

            {
                // Ошибка: исключение сработает при любом ложном срабатывании \t.
                throw new FileLoadException($"Файл {pathToCsv} повреждён и не может быть обработан.");
            }

            DirectoryItem.ItemTypes tempType;
            switch (tempStr[0].Trim('"'))
            {
                case "File":
                    tempType = DirectoryItem.ItemTypes.File;
                    break;
                case "Directory":
                    tempType = DirectoryItem.ItemTypes.Directory;
                    break;
                default:
                    throw new FileLoadException($"Файл {pathToCsv} повреждён.");
            }

            items.Add(new DirectoryItem(tempType, tempStr[1], Convert.ToDateTime(tempStr[2].Trim('"'))));
        }

        return items;
    }
}