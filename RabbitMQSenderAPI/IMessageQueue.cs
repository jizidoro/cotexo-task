namespace RabbitMQSenderAPI;

public interface IMessageQueue
{
    void Send(string message);
}