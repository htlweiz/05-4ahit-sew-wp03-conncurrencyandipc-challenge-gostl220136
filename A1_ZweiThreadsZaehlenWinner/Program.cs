using System;
using System.Threading;

namespace A1_ZweiThreadsZaehlenWinner;

class Program
{
    private static int threadAValue = 1;
    private static int threadBValue = 100;
    private static bool stopped = false;

    public static void Main(string[] args)
    {
        Console.WriteLine("Übung 1: Zwei Threads - Zählen & Winner");

        Thread threadA = new Thread(CountUpThreadA);
        Thread threadB = new Thread(CountDownThreadB);
        threadA.Start();
        threadB.Start();
        threadA.Join();
        threadB.Join();

        if (threadAValue < 50)
        {
            Console.WriteLine($"Thread B {threadBValue}");
        }
        else if (threadAValue == 50)
        {
            Console.WriteLine($"Tie {threadAValue} {threadBValue}");
        }
        else
        {
            Console.WriteLine($"Thread A {threadAValue}");
        }
    }
    
    private static void CountUpThreadA()
    {
        for (int i = 1; i <= 100; i++)
        {
            if (stopped) break;
            threadAValue = i;

            if (threadAValue == threadBValue)
            {
                Console.WriteLine($"{threadAValue} {threadBValue}");
                stopped = true;
            }
            Thread.Sleep(100);
        }
    }

    private static void CountDownThreadB()
    {
        for (int i = 100; i >= 1; i--)
        {
            if (stopped) break;
            threadBValue = i;
            
            if (threadAValue == threadBValue)
            {
                Console.WriteLine($"{threadAValue} {threadBValue}");
                stopped = true;
            }
            Thread.Sleep(100);
        }
    }
}