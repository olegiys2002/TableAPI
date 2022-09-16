
namespace Core.IServices
{
    public interface IRabbitMqService
    {
        void SendMessage<T>(T message);
    }
}
