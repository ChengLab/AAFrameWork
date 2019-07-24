using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Framing;

namespace AA.RabbitMQ
{
     public class MqConfigDom
    {
        public MqConfigDom()
        {

            //MqHost = "localhost";
            //MqUserName = "yy";
            //MqPassword = "hello!";
            //MqHost = ConfigurationManager.AppSettings["RabbitMQ_HostName"];
            //MqUserName = ConfigurationManager.AppSettings["RabbitMQ_UserName"];
            //MqPassword = ConfigurationManager.AppSettings["RabbitMQ_Password"];
            MqPort = 5672;
        }

        public string MqHost { get; set; }
        public string MqUserName { get; set; }
        public string MqPassword { get; set; }
        public int MqPort { get; set; }

    }
}
