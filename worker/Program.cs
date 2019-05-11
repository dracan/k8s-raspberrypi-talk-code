using System;
using System.Threading;
using web;

namespace worker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RabbitMqMessageQueue.Init();

                RabbitMqMessageQueue.Subscribe("MyQueue", async x =>
                {
                    Console.WriteLine($"Received message {x}");
                    await SlackHelper.SendSlackMessage(x);
                });

                Console.WriteLine("Waiting for messages...");
                Thread.Sleep(Timeout.Infinite);
            }
            finally
            {
                RabbitMqMessageQueue.Cleanup();
            }
        }
    }
}
