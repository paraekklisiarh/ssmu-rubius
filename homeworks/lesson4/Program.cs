namespace lesson4;

static class Program
{
    private static void Main()
    {
        var handler1 = new HandlerOne();
        var handler2 = new HandlerTwo();
        var counter = new Counter();

        counter.ItIs79 += handler1.Message;
        counter.ItIs79 += handler2.Message;

        counter.Count79();
    }
}