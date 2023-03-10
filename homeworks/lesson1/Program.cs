public class Program

{
    public static void Main()
    {

        LessonOne.Hello();
        
    }
}

public class LessonOne
{
    public static void Hello()
    {
        Console.WriteLine("Введите имя:");
        
        var name = Console.ReadLine();
        
        Console.WriteLine(name + ", привет!");

    }
}