using System;
using System.Threading;

namespace A1_ZweiThreadsZaehlenWinner;

class Program
{
    private static int threadAValue = 0;
    private static int threadBValue = 0;

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
        for (int i = 1; i <= 100; i++)
        {
            threadAValue = i;

            if (threadAValue == threadBValue)
            {
                Console.WriteLine($"{threadAValue} {threadBValue}");
            }
            Thread.Sleep(100);
        }
    }

    private static void CountDownThreadB()
    {
        for (int i = 100; i >= 1; i--)
        {
            threadBValue = i;
            
            if (threadAValue == threadBValue)
            {
                Console.WriteLine($"{threadAValue} {threadBValue}");
            }
            Thread.Sleep(100);
        }
    }

}

