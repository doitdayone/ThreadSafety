using System;

class Program
{
    static int active;
    static Semaphore semaphore = new Semaphore(2, 3); // cho phép 2 thread cùng lúc

    static void PrintJob(object id)
    {
        Console.WriteLine($"Thread {id} is waiting...");
        semaphore.WaitOne();

        int now = Interlocked.Increment(ref active);
        Console.WriteLine($"Thread {id} is printing... (active={now})");

        Thread.Sleep(2000);

        now = Interlocked.Decrement(ref active);
        Console.WriteLine($"Thread {id} is done. (active={now})");

        semaphore.Release();
    }

    static void Main()
    {
        semaphore.Release();
        for (int i = 1; i <= 10; i++)
        {
            new Thread(PrintJob).Start(i);
        }
    }
}
