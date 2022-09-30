namespace Core.Models.Rabbit
{
    public class RabbitMqOptions
    {
        public const string RabbitMq = "RabbitMq";
        public string HostName { get; set; }
        public string Queue { get; set; }
    }
}
