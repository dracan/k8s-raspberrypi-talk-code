using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace web
{
    public static class RabbitMqMessageQueue
    {
        private static IConnection _connection;
        private static IModel _channel;

        public static void Init()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public static void Cleanup()
        {
            _connection?.Dispose();
            _channel.Dispose();
        }

        public static void Send(string queueName, string message)
        {
            _channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));
        }

        public static void Subscribe(string queueName, Func<string, Task> callbackAsync)
        {
            _channel.QueueDeclare(queueName, false, false, false, null); // Ensure Queue Exists

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                await callbackAsync(message);
            };

            _channel.BasicConsume(queueName, true, consumer);
        }
    }
}
