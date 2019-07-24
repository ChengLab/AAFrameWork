using AA.FrameWork.Tests.Unit.RabbitMQ.ServiceBus.meesageContracts;
using AA.ServiceBus;
using MassTransit;
using MassTransit.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AA.FrameWork.Tests.Unit.RabbitMQ.ServiceBus
{
    public class Producer
    {
        [Fact]
        public void TestProducer()
        {
            string rabbitMqUri = "rabbitmq://localhost:5672";
            string rabbitMqUserName = "";
            string rabbitMqPassword = "";
         

            PulishEvent(rabbitMqUri, rabbitMqUserName, rabbitMqPassword);
            SendCommand(rabbitMqUri, rabbitMqUserName, rabbitMqPassword);
        }


        private void PulishEvent(string rabbitMqUri, string rabbitMqUserName, string rabbitMqPassword)
        {
            IBusControl busControl = ServiceBusManager.Instance.UseRabbitMq(rabbitMqUri, rabbitMqUserName, rabbitMqPassword)
                         .BuildEventProducer();

            TaskUtil.Await(busControl.Publish<OrderSubmitted>(new
            {
                Id = 1
            }));
        }   

        private void SendCommand(string rabbitMqUri, string rabbitMqUserName, string rabbitMqPassword)
        {
            string queueName = "order.queue";

            ISendEndpoint busControl = ServiceBusManager.Instance.UseRabbitMq(rabbitMqUri, rabbitMqUserName, rabbitMqPassword)
                         .BuildCommandProducer(queueName);

            TaskUtil.Await(busControl.Send<SubmitOrder>(new
            {
                Id = 10
            }));
        }
    }

}
