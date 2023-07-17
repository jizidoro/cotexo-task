using RabbitMQ.Client;

namespace hexagonal.API;

public interface IRabbitMQConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}