using AA.ServiceBus.Configuration;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AA.ServiceBus
{
   public class ServiceBusManager
    {
        private static readonly Lazy<ServiceBusManager> _Instance = new Lazy<ServiceBusManager>(() => new ServiceBusManager());
        public static ServiceBusManager Instance => _Instance.Value;
        public ServiceBusConfiguration ServiceBusConfiguration { get; set; }
        private IBusControl _bus;
        private ServiceBusManager()
        {
            ServiceBusConfiguration = new ServiceBusConfiguration();
        }



        public ServiceBusManager UseRabbitMq(string rabbitMqUri, string rabbitMqUserName, string rabbitMqPassword)
        {
            ServiceBusConfiguration.RabbitMqUri = rabbitMqUri;
            ServiceBusConfiguration.RabbitMqUserName = rabbitMqUserName;
            ServiceBusConfiguration.RabbitMqPassword = rabbitMqPassword;
            return this;
        }

        public ServiceBusManager RegisterConsumer<TConsumer>(string queueName = null) where TConsumer : class, IConsumer, new()
        {
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> action = (cfg, host) =>
            {
                if (queueName == null)
                {
                    cfg.ReceiveEndpoint(host, ConfigureReceiveEndpoint<TConsumer>());
                }
                else
                {
                    cfg.ReceiveEndpoint(host, queueName, ConfigureReceiveEndpoint<TConsumer>());
                }
            };

            ServiceBusConfiguration.BeforeBuildActions.Add(action);

            return this;
        }


        public ISendEndpoint BuildCommandProducer(string queueName)
        {
            _bus = Build();

            if (!ServiceBusConfiguration.RabbitMqUri.EndsWith("/"))
            {
                queueName = queueName.Insert(0, "/");
            }

            var sendToUri = new Uri($"{ServiceBusConfiguration.RabbitMqUri}{queueName}");

            return TaskUtil.Await(() => _bus.GetSendEndpoint(sendToUri));
        }

        public IBusControl BuildEventProducer()
        {
            _bus = Build();
            return _bus;
        }

        public IBusControl Build()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(ServiceBusConfiguration.RabbitMqUri), hst =>
                {
                    hst.Username(ServiceBusConfiguration.RabbitMqUserName);
                    hst.Password(ServiceBusConfiguration.RabbitMqPassword);
                });

                foreach (Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> action in ServiceBusConfiguration.BeforeBuildActions)
                {
                    action.Invoke(cfg, host);
                }
            });
        }


        public BusHandle Start()
        {
            return TaskUtil.Await(() => _bus.StartAsync());
        }


        private Action<IRabbitMqReceiveEndpointConfigurator> ConfigureReceiveEndpoint<TConsumer>() where TConsumer : class, IConsumer, new()
        {
            return _ =>
            {
                _.Consumer<TConsumer>();
            };
        }

    }
}
