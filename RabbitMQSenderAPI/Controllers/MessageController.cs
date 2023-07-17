using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQSenderAPI.Contracts;

namespace RabbitMQSenderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageQueue _messageQueue;
    private readonly IServiceProvider _serviceProvider;

    public MessageController(IMessageQueue messageQueue, IServiceProvider serviceProvider)
    {
        _messageQueue = messageQueue;
        _serviceProvider = serviceProvider;
    }

    [HttpPost]
    public IActionResult Post([FromBody] BookDto book)
    {
        var rabbitMQConnection = _serviceProvider.GetRequiredService<IRabbitMQConnection>();
        using (var channel = rabbitMQConnection.CreateModel())
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(book));

            channel.BasicPublish(exchange: "",
                routingKey: "BookQueue",
                mandatory: false,
                basicProperties: null,
                body: body);
        }

        return Ok();
    }
}