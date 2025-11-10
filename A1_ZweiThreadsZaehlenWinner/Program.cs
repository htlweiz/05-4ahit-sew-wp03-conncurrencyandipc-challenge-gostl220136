using System;
using System.Threading;

namespace A1_ZweiThreadsZaehlenWinner;

class Program
{
   
    
    public static void Main(string[] args)
    {
        Console.WriteLine("Übung 1: Zwei Threads – Zählen & Winner");

        Thread threadA = new Thread(CountUpThreadA);
        Thread threadB = new Thread(CountDownThreadB);
        threadA.Start();
        threadB.Start();
        threadA.Join();
        threadB.Join();
        
        
    }
    
    private static void CountUpThreadA()
    {
        for (int i = 0; i <= 100; i++)
        {
            Console.WriteLine($"Thread A: {i}");
            Thread.Sleep(100);
        }
    }
    
    private static void CountDownThreadB()
    {
        for (int i = 100; i >= 0; i--)
        {
            Console.WriteLine($"Thread B: {i}");
            Thread.Sleep(100);
        }
    }
}

