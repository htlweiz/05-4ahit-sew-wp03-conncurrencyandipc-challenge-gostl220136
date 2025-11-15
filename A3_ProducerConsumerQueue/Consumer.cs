using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace A3_ProducerConsumerQueue;

public class Consumer
{
    private volatile bool shouldStop = false;
    private Thread? consumerThread;
    private int sum = 0;
    private readonly object lockObj = new object();

    public Consumer(ConcurrentQueue<int> queue)
    {
        
        // Thread im Konstruktor starten
        consumerThread = new Thread(() => ConsumeNumbers(queue));
        consumerThread.Start();
    }

    private void ConsumeNumbers(ConcurrentQueue<int> queue)
    {
        while (!shouldStop)
        {
            // TODO
            lock (lockObj)
            {
                if (queue.TryDequeue(out int number))
                {
                    sum += number;
                    //Console.WriteLine($"Consumer consumed: {number}");
                }   
            }
            Thread.Sleep(250); // 250ms Takt
        }
    }

    public void Stop()
    {
        shouldStop = true;
    }

    public int GetSum()
    {
        return sum;
    }
}
