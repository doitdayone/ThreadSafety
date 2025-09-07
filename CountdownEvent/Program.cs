class Program
{
    static CountdownEvent countdown = new CountdownEvent(3);

    static void Worker(object id)
    {
        Console.WriteLine($"Thread {id} started working...");
        Thread.Sleep(2000); // giả lập làm việc
        Console.WriteLine($"Thread {id} finished.");
        countdown.Signal(); // báo hiệu 1 công việc đã xong
    }

    static void Main()
    {
        for (int i = 1; i <= 3; i++)
            new Thread(Worker).Start(i);

        Console.WriteLine("Main thread is waiting for workers...");
        countdown.Wait(); // chờ đủ 3 tín hiệu
        Console.WriteLine("All tasks completed. Proceeding to next step!");
    }
}
