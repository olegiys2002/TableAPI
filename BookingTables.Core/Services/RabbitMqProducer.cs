using Core.IServices;
using Core.Models.Rabbit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Core.Services
{
    public class RabbitMqProducer : IRabbitMqService
    {
        private readonly RabbitMqOptions _options;
        public RabbitMqProducer(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }
        public void SendMessage<T>(T message)
        {
        
            var factory = new ConnectionFactory { HostName = _options.HostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(_options.Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(message);    
            var bytes = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "users", body: bytes);


        }
    }
}
