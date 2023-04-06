namespace lesson4;

public class Counter
{
    public delegate void Nd(int count);

    public event Nd? ItIs79;

    public void Count79()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i == 79)
            {
                ItIs79?.Invoke(79);
            }
        }
    }
}