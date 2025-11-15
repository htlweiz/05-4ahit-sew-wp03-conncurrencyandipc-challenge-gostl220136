using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace NamedPipeClientApp;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Named Pipe Client ===");
        Console.WriteLine("1. Start Client");
        Console.WriteLine("2. Exit");
        var choice = Console.ReadLine();
        if (choice == "1")
        {
            var client = new NamedPipeClient();
            client.StartClient();
        }
    }
}

class NamedPipeClient
{
    private const string PipeName = "PingPongPipe";

    public void StartClient()
    {
        using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut))
        {
            Console.WriteLine("Client started. Connecting to server...");
            Console.WriteLine($"Pipe Name: {PipeName}\n");
            pipeClient.Connect();
            Console.WriteLine("Connected to server.");

            try
            {
                using (StreamReader reader = new StreamReader(pipeClient))
                {
                    using (StreamWriter writer = new StreamWriter(pipeClient))
                    {
                        writer.AutoFlush = true;
                        string? message;
                        int round = 0;
                        while ((message = reader.ReadLine()) != null)
                        {
                            Console.WriteLine($"Recieved: \"{message}\"");
                            Thread.Sleep(250);
                            string clientMessage = $"Pong {round}";
                            writer.WriteLine(clientMessage);
                            Console.WriteLine($"Sent: \"{clientMessage}\"");
                            round++;
                        }
                    }
                }  
            }
            catch (IOException e)
            {
                Console.WriteLine("ERROR: {0} Server closed connection?", e.Message); 
            }
        }
    }
}