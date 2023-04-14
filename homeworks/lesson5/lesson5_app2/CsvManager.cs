namespace lesson5_app2;

public static class CsvManager
{
    private const string Separator = "\"\\t\"";

    /// <summary>
    /// Метод, считывающий построчно CSV и преобразующий его в объекты класса DirectoryItem
    /// </summary>
    /// <param name="pathToCsv"></param>
    /// <returns>Список из объектов класса <see cref="DirectoryItem" /> </returns>
    /// <exception cref="FileLoadException"></exception>
    public static List<DirectoryItem> GetItemsFromCsv(string pathToCsv)
        // Можно разделить на два метода: CsvLinesParseToItem() и CsvLinesRead(),
        // но лень обрабатывать ошибки дважды.
    {
        var items = new List<DirectoryItem>();

        // Для получения построчно таблиц из файла ReadLines предпочтительнее ReadAllLines
        // т.к. тратит на порядок меньше ресурсов, считывая буквально построчно.
        var csv = File.ReadLines(pathToCsv);

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