 class Program
{
    static Mutex mutex = new Mutex(true, "Resource");

    static void AccessResource()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} waiting to acquire Mutex...");
        mutex.WaitOne(); // Acquire the mutex
        Console.WriteLine($"{Thread.CurrentThread.Name} has acquired the Mutex.");

        Thread.Sleep(2000); // Simulate work

        Console.WriteLine($"{Thread.CurrentThread.Name} releasing Mutex.");
        mutex.ReleaseMutex(); // Release the mutex
    }

    static void Main()
    {
        Console.WriteLine("Main acquired.");
        Thread.Sleep(2000);
        Console.WriteLine("Main releasing...");
        mutex.ReleaseMutex();

        for (int i = 1; i <= 3; i++)
        {
            Thread t = new Thread(AccessResource);
            t.Name = $"Thread {i}";
            t.Start();
        }
    }
}