using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.RabbitMQ.ServiceBus.meesageContracts
{
    public interface SubmitOrder
    {
        long Id { get; set; }
        Decimal OrderPrice { get; set; }
    }
}
