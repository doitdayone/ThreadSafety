
class Program
{
    static SpinLock spinLock = new SpinLock();

    static void Work(object id)
    {
        bool lockTaken = false;
        try
        {
            spinLock.Enter(ref lockTaken);
            Console.WriteLine($"Thread {id} acquired spinlock.");
            Thread.Sleep(500); // giả lập critical section rất ngắn
        }
        finally
        {
            if (lockTaken)
            {
                spinLock.Exit();
                Console.WriteLine($"Thread {id} released spinlock.");
            }
        }
    }

    static void Main()
    {
        for (int i = 1; i <= 5; i++)
        {
            var t = new Thread(Work);
            t.Start(i);
        }
    }
}
