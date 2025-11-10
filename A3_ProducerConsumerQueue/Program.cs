using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace A3_ProducerConsumerQueue;

class Program
{
    public static void Main(string[] args)
    {
        var queue = new ConcurrentQueue<int>();

        Console.WriteLine("Übung 3: Producer-Consumer");
        Console.WriteLine("==========================================\n");

        // TODO
        List<Producer> producers = new List<Producer>();
        for (int i = 0; i < 5; i++)
        {
            producers.Add(new Producer(i + 1, queue));
        }

        Console.WriteLine("Producer und Consumer gestartet...\n");

        // Überwachung: Jede Sekunde Queue-Füllstand ausgeben und auf >50 prüfen

        // TODO
        while (queue.Count < 50)
        {
            Thread.Sleep(1000);
        }
        foreach (var producer in producers)
        {
            Console.WriteLine($"{queue.Count}");
            producer.Stop();
        }

    
        Consumer consumer1 = new Consumer(queue);

        while (queue.Count > 0)
        {
            Thread.Sleep(250);
            Console.Write(".");
        }
        Console.WriteLine($"Alle Zahlen konsumiert. Endsumme: {consumer1.GetSum()}");
        consumer1.Stop();
    }
}
