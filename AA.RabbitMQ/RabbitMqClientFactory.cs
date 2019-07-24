using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace AA.RabbitMQ
{
    public class RabbitMqClientFactory
    {
        public static IConnection CreateConnection()
        {
            var mqConfigDom = MqConfigDomFactory.CreateConfigDomInstance();//获取MQ配置
             
            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigDom.MqHost,
                Port = mqConfigDom.MqPort,
                UserName = mqConfigDom.MqUserName,
                Password = mqConfigDom.MqPassword,
                RequestedHeartbeat = heartbeat, //心跳超时时间
                AutomaticRecoveryEnabled = true,//自动重连
                //VirtualHost = "/"
            };
            return factory.CreateConnection(); 
        }

        public static IModel CreateModel(IConnection connection)
        {
            return connection.CreateModel();
        }
    }
}
