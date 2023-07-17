using System.Threading;
using hexagonal.Application.Components.BookComponent.Commands;
using hexagonal.Application.Components.BookComponent.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace hexagonal.API;

public class BookConsumerService : BackgroundService
{
    private readonly IRabbitMQConnection _rabbitMQConnection;
    private readonly IServiceProvider _services;

    public BookConsumerService(IRabbitMQConnection rabbitMQConnection, IServiceProvider services)
    {
        _rabbitMQConnection = rabbitMQConnection;
        _services = services;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_rabbitMQConnection.CreateModel());

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            var book = JsonConvert.DeserializeObject<BookCreateDto>(message);

            // Criar um escopo para resolver IBookCommand
            using (var scope = _services.CreateScope())
            {
                var bookCommand = scope.ServiceProvider.GetRequiredService<IBookCommand>();

                // Chamar o método na application
                bookCommand.Create(book);
            }
        };

        _rabbitMQConnection.CreateModel().BasicConsume(queue: "BookQueue",
                             autoAck: true,
                             consumer: consumer);

        return Task.CompletedTask;
    }
}