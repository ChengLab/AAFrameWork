using AA.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using System.Threading.Tasks;
using AA.FrameWork.Tests.Unit.RabbitMQ.ServiceBus.meesageContracts;
using Xunit;
using AA.Log4Net;
using AA.FrameWork.Logging;

namespace AA.FrameWork.Tests.Unit.RabbitMQ.ServiceBus
{
    public class Consumer
    {
        [Fact]
        public void TestConsumer()
        {
            Log4NetLogger.Use("Log4Net/log4net.config");
            string rabbitMqUri = "rabbitmq://localhost:5672";
            string rabbitMqUserName = "";
            string rabbitMqPassword = "";
            string queueName = "order.queue";

            var busControl = ServiceBusManager.Instance.UseRabbitMq(rabbitMqUri, rabbitMqUserName, rabbitMqPassword)
             .RegisterConsumer<SubmitOrderCommandConsumer>(queueName)
             .RegisterConsumer<OrderSubmittedEventConsumer>(null)
             .Build();
            busControl.Start();

        }
    }

    public class OrderSubmittedEventConsumer : IConsumer<OrderSubmitted>
    {
        public async Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            var @event = context.Message;

            var result = $"OrderSubmittedEvent {@event.Id.ToString()}";
            ILog log = Logger.Get(typeof(OrderSubmittedEventConsumer));
            log.Debug(result);
            //do somethings...
        }
    }


    public class SubmitOrderCommandConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            var command = context.Message;

            var result = $"CreateFooCommand {command.Id.ToString()}";
            ILog log = Logger.Get(typeof(SubmitOrderCommandConsumer));
            log.Debug(result);
            //do somethings...
        }
    }

}
