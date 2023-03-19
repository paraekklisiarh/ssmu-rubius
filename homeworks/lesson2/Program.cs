/* УСЛОВИЕ ЗАДАЧИ:
 * Сделать простое консольное приложение, которое находит второй наибольший элемент в массиве целых чисел (int).
 * Например для массива { 1, 3, 5, 2, 4 } вторым наибольшим элементом будет 4.
 * Для массива {1, 5, 5, 2, 4 } вторым наибольшим элементом также должно быть 4. Обратите на это внимание при разработке алгоритма.
 *
 * Приложение должно запрашивать у пользователя размер массива, а затем значение каждого элемента в массиве, а затем выдавать результат вычисления.
 *
 * Особое внимание обратите на обработку ошибок пользовательского ввода - это основной критерий оценки для данного задания.
 */

/*
 * Варианты решения ошибки: массив заполнен одним числом.
 * №1. Принудить пользователя заполнять массив разными числами.
 *     Ок, если задача с про ввод из консоли, как сейчас, "защита от дурака" работает.
 *     А если получать массив откуда-нибудь из БД - не ок.
 *
 * №2. Вывести сообщение об этом и пусть пользователь сам решает, дурак он или так и было задумано.
 *  2.1 Создать исключение: ArrayFilledOneNumExeption и обрабатывать его при вызове функции:
 *      try { ArraySecondMaxNum(); }
 *      catch (ArrayFilledOneNumExeption e) {Console.WriteLine(e)}
 *       
 *  2.2 Возвращать bool(массив заполнен разными числами). Тогда функция выглядит так:
 *      static (int maxNum, bool isArrayFilledOneNum) ArraySecondMaxNum()
 *      { return(maxNum:maxNum, isArrayFilledOneNum:(условие) }
 *      Говнокод получается.
 */
public class lesson2

{
    
    public static void Main()
    {
        Console.WriteLine("Введите длину массива: целое число больше или равное 2.");
        int arrLength = 0;
        
        // Защита от некорректного ввода длины массива.
        while (arrLength < 2)
        {
            arrLength = ReadInt();
            if (arrLength < 2)
            {
                Console.WriteLine("Вводите целое положительное число, больше или равное 2.");
            }
        }
        
        var arrNum = GetIntArrayFromConsole(arrLength);
        
        
        // "Защита от дурака": массив заполнен одним числом?
        if (arrNum.Distinct().ToArray().Length == 1)
        {
            // Заставляем пользователя заполнить массив разными числами.
            while(arrNum.Distinct().ToArray().Length == 1)
            {
                Console.WriteLine("Вы заполнили массив одним числом. Они все одинаковые! Заполните разными.");
                arrNum = GetIntArrayFromConsole(arrLength);
            }
        }
        
        
        /*
        // Вариант с обработкой исключения: массив заполнен одним числом.
        int maxNum = 0;
        try
        {
            maxNum = ArraySecondMaxNum(arrNum);
            Console.WriteLine ("Второй максимальный элемент массива: " + maxNum);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        */

        Console.WriteLine("Второй максимальный элемент массива" + ArraySecondMaxNum(arrNum));
    }

    static int ArraySecondMaxNum(int[] arrNum)
    {
            // Функция поиска второго максимального элемента массива.    
        
            // Сортировка массива: второе максимальное число должно быть arrNum[1]
            Array.Sort(arrNum);
            Array.Reverse(arrNum);
            // Устранение повторяющихся элементов
            arrNum = arrNum.Distinct().ToArray();
            
            /*
            // Создание исключения: массив заполнен одним числом. Найти второй максимальный элемент невозможно.
            if (arrNum.Length == 1)
            {
                throw new Exception("Массив заполнен одним числом. Передайте массив, заполненный разными числами.");
            }
            */
            
            return arrNum[1];
    }

    static int ReadInt()
    {
        // Функция для получения из консоли целого числа.
        
        int value = 0;
        bool isFormatRight = false;

        while (!isFormatRight)
        {
            if (int.TryParse(Console.ReadLine(), out value))
            {
                isFormatRight = true;
            }
            else
            {
                Console.WriteLine("Введите целое число от -2147483648 до 2147483647");
            }
        }
        return value;
    }

    
    static int[] GetIntArrayFromConsole(int lenght)
    {
        // Функция заполнения массива целыми числами с клавиатуры.
        // На входе - длина массива.
        
        var array = new int[lenght];
        Console.WriteLine("Введите массив целых чисел: каждое число с новой строки.");

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = ReadInt();
        }

        return array;
    }
}