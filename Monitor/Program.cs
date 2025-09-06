
class Program
{
    private static readonly Queue<int> _queue = new Queue<int>();
    private static readonly object _lock = new object();
    private static bool _done = false;
    private static Random _random = new Random();

    static void Main()
    {
        // Start consumer
        var consumer = new Thread(Consume);
        consumer.Start();

        // Start producer
        var producer = new Thread(Produce);
        producer.Start();

        //producer.Join();
        //consumer.Join();
    }

    static void Produce()
    {
        for (int i = 1; i <= 10; i++)
        {
            lock (_lock)
            {
                _queue.Enqueue(i);
                Console.WriteLine($"Producer: add {i}");

                // Báo cho consumer biết có dữ liệu mới
                Monitor.Pulse(_lock);
            }
            Thread.Sleep(_random.Next(1000,2000)); // giả lập thời gian sản xuất
        }

        lock (_lock)
        {
            _done = true;
            Monitor.PulseAll(_lock); // báo cho consumer thoát
        }
    }

    static void Consume()
    {
        while (true)
        {
            int item;
            lock (_lock)
            {
                // Nếu queue rỗng và producer chưa xong, thì chờ
                while (_queue.Count == 0 && !_done)
                {
                    Console.WriteLine("Consumer: no data, waiting...");
                    Monitor.Wait(_lock); // nhả lock và chờ tín hiệu
                }

                if (_queue.Count == 0 && _done)
                {
                    Console.WriteLine("Consumer: out of data, ended.");
                    return;
                }

                item = _queue.Dequeue();
            }

            Console.WriteLine($"Consumer: processing {item}");
            Thread.Sleep(1500); // giả lập thời gian xử lý
        }
    }
}
