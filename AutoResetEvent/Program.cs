class Program
{
    /*
        All 3 threads start and call WaitOne() — they are blocked.
        The Main thread calls Set() 3 times — each time exactly one thread proceeds.
        After allowing a thread through, AutoResetEvent automatically resets to non-signaled state.
    */
    static AutoResetEvent autoEvent = new AutoResetEvent(false); // Initially non-signaled

    static void Worker(object id)
    {
        Console.WriteLine($"Thread {id} waiting...");
        autoEvent.WaitOne(); // Wait until signaled
        Console.WriteLine($"Thread {id} passed through turnstile.");
    }

    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            new Thread(Worker).Start(i);
        }

        Thread.Sleep(1000); // Give threads time to start and wait

        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"Main thread allows one thread to pass...");
            autoEvent.Set(); // Allow one thread to proceed
            //Thread.Sleep(1000); // Wait before allowing the next
        }
    }
}