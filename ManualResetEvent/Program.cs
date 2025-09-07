class Program
{
    static ManualResetEvent manualEvent = new ManualResetEvent(false);

    static void Worker(object id)
    {
        Console.WriteLine($"Thread {id} waiting...");
        manualEvent.WaitOne(); // chờ tín hiệu
        Console.WriteLine($"Thread {id} passed through gate.");
    }

    static void Main()
    {
        // 3 thread đầu tiên
        for (int i = 1; i <= 3; i++)
            new Thread(Worker).Start(i);

        Thread.Sleep(1000); // chờ threads vào WaitOne()

        Console.WriteLine("Main opens the gate!");
        manualEvent.Set(); // mở cổng => tất cả 3 thread qua cùng lúc

        Thread.Sleep(500); // chờ các thread chạy xong

        Console.WriteLine("Main closes the gate.");
        manualEvent.Reset(); // đóng cổng

        // Thread 4
        new Thread(Worker).Start(4);
        Thread.Sleep(1000);

        Console.WriteLine("Main opens the gate again!");
        manualEvent.Set(); // mở cổng lần 2 => Thread 4 qua
    }
}
