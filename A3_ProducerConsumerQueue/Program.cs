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
        Console.WriteLine("Starte Producer und Consumer...\n");

        List<Producer> producers = new List<Producer>();
        for (int i = 0; i < 5; i++)
        {
            producers.Add(new Producer(i + 1, queue));
        }

        Consumer consumer1 = new Consumer(queue);

       

        // Überwachung: Jede Sekunde Queue-Füllstand ausgeben und auf >50 prüfen
        while (queue.Count < 50)
        {
            Console.WriteLine($"\n{new string('~',27)}\n    Queue-Füllstand: {queue.Count}\n{new string('~',27)}\n");
            Thread.Sleep(1000);
        }
        foreach (var producer in producers)
        {
            producer.Stop();
        }
        Console.WriteLine($"\n{new string('#',25)}\n End Queue-Füllstand: {queue.Count}\n{new string('#',25)}\n");

        // Warten bis alle Zahlen konsumiert sind
        bool done = false;
        while (!done)
        {
            if (queue.Count == 0)
            {
                Console.WriteLine($"\nAlle Zahlen konsumiert. Endsumme: {consumer1.GetSum()}");
                consumer1.Stop();            
                done = true;
            }
        }
    }
}
