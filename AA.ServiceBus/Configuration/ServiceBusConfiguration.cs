using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;

namespace AA.ServiceBus.Configuration
{
    public class ServiceBusConfiguration
    {
        public string RabbitMqUri { get; set; }
        public string RabbitMqUserName { get; set; }
        public string RabbitMqPassword { get; set; }

        public int? TripThresholdForCircuitBreaker { get; set; }
        public int? ActiveThresholdForCircuitBreaker { get; set; }
        public TimeSpan? ResetIntervalForCircuitBreaker { get; set; }

        public int? RateLimit { get; set; }
        public TimeSpan? RateLimitInterval { get; set; }

        public ushort? PrefetchCount { get; set; }

        public TimeSpan DefaultRequestTimeoutTime { get; set; }

        public bool UseMessageScheduler { get; set; }
        public string QuartzEndpoint { get; set; }
        public bool UseDelayedExchangeMessageScheduler { get; set; }

        public int? UseConcurrentConsumerLimit { get; set; }

        public List<Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>> BeforeBuildActions { get; set; }

        public ServiceBusConfiguration()
        {
            DefaultRequestTimeoutTime = TimeSpan.FromSeconds(20);
            BeforeBuildActions = new List<Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>>();
        }
    }
}