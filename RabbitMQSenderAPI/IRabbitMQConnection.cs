using RabbitMQ.Client;

namespace RabbitMQSenderAPI;

public interface IRabbitMQConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}