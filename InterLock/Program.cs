class Program
{
    static int counter = 0;

    static void IncrementUnsafe()
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            counter++; // không nguyên tử -> sai số
        }
    }

    static void IncrementSafe()
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            Interlocked.Increment(ref counter); // nguyên tử
        }
    }

    static void Main()
    {
        counter = 0;
        var t1 = new Thread(IncrementUnsafe);
        var t2 = new Thread(IncrementUnsafe);

        t1.Start(); t2.Start();
        t1.Join(); t2.Join();
        Console.WriteLine($"Unsafe result: {counter}");

        counter = 0;
        t1 = new Thread(IncrementSafe);
        t2 = new Thread(IncrementSafe);

        t1.Start(); t2.Start();
        t1.Join(); t2.Join();
        Console.WriteLine($"Safe result: {counter}");
    }
}
