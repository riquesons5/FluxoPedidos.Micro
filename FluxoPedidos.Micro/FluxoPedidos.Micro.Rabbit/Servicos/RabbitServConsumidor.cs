using FluxoPedidos.Micro.Application.Pedidos;
using FluxoPedidos.Micro.Application.Pedidos.Dtos;
using FluxoPedidos.Micro.Rabbit.Configuracoes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FluxoPedidos.Micro.Rabbit.Servicos
{
    public class RabbitServConsumidor : BackgroundService
    {
        private IModel? _channel;
        private IConnection? _connection;
        private readonly RabbitConfig _config;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RabbitServConsumidor> _logger;

        public RabbitServConsumidor(IOptions<RabbitConfig> config,
                                    ILogger<RabbitServConsumidor> logger,
                                    IServiceScopeFactory scopeFactory)
        {
            _config = config.Value;
            _logger = logger;
            _scopeFactory = scopeFactory;

            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                UserName = _config.UserName,
                Password = _config.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.BasicQos(0, 1, false);

            _channel.QueueDeclare(
                queue: _config.FilaPedido,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(
                queue: _config.FilaPedidoDeadLetter,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                using var scope = _scopeFactory.CreateScope();
                var aplicPedido = scope.ServiceProvider.GetRequiredService<IAplicPedido>();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var pedido = JsonSerializer.Deserialize<PedidoDto>(message, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (pedido is null)
                        throw new Exception("Mensagem inválida: não foi possível converter para PedidoDto");

                    await aplicPedido.CriarPedidoAsync(pedido);

                    _channel.BasicAck(ea.DeliveryTag, false);

                    _logger.LogInformation("Pedido processado com sucesso: {@Pedido}", pedido);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar mensagem: {Mensagem}", message);

                    if (_channel is not null)
                    {
                        var properties = _channel.CreateBasicProperties();
                        properties.Persistent = true;

                        properties.Headers ??= new Dictionary<string, object>();
                        properties.Headers["Erro"] = ex.Message;

                        _channel.BasicPublish(
                            exchange: "",
                            routingKey: _config.FilaPedidoDeadLetter,
                            basicProperties: properties,
                            body: body
                        );

                        _channel.BasicAck(ea.DeliveryTag, false);

                        _logger.LogWarning("Mensagem redirecionada para DeadLetter: {Mensagem}", message);
                    }
                }
            };

            _channel.BasicConsume(
                queue: _config.FilaPedido,
                autoAck: false,
                consumer: consumer);

            return Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}