using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.RabbitMQ.ServiceBus.meesageContracts
{
    public interface OrderSubmitted
    {
        long Id { get; set; }
        decimal OrderPrice { get; set; }
    }
}
