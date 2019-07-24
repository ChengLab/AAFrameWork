using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.RabbitMQ
{
    public class MqConfigDomFactory
    {
        public static MqConfigDom CreateConfigDomInstance()
        {
            return new MqConfigDom();
        }
    }
}
