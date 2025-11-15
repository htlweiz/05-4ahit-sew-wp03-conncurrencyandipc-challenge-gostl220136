using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace NamedPipeServerApp;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Named Pipe Server ===");
        Console.WriteLine("1. Start Server");
        Console.WriteLine("2. Exit");
        var choice = Console.ReadLine();
        if (choice == "1")
        {
            var server = new NamedPipeServer();
            server.StartServer();
        }
    }
}

class NamedPipeServer
{
    private const string PipeName = "PingPongPipe";
    
    public void StartServer()
    {
        using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeName))
        {
            Console.WriteLine("Server started. Waiting for client...");
            Console.WriteLine($"Pipe Name: {PipeName}\n");

            pipeServer.WaitForConnection();
            Console.WriteLine("Client connected.");

            using (StreamReader reader = new StreamReader(pipeServer))
            {
                using (StreamWriter writer = new StreamWriter(pipeServer))
                {
                    int round = 0;
                    writer.AutoFlush = true;
                    string serverMessage = $"Ping {round}";
                    writer.WriteLine(serverMessage);
                    Console.WriteLine($"<Server> {serverMessage}");
                    round++;

                    string message;
                    do 
                    {
                        if ((message = reader.ReadLine()) != null)
                        {
                            Console.WriteLine($"<Client> {message}");
                            Thread.Sleep(1000);
                            serverMessage = $"Ping {round}";
                            writer.WriteLine(serverMessage);
                            Console.WriteLine($"<Server> {serverMessage}");
                            round++;
                        }
                    } while (round <= 10);

                    // Read final message from client
                    if ((message = reader.ReadLine()) != null)
                    {
                        Console.WriteLine($"<Client> {message}");
                    }
                }                    
            }
        }       
    }
}
