using AA.FrameWork.Logging;
using AA.Log4Net;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AA.FrameWork.Tests.Unit.Log4Net
{
    public class log4netTest
    {

        [Fact]
        public void TestLog4net()
        {
            Log4NetLogger.Use("Log4Net/log4net.config");
            ILog log = Logger.Get(typeof(log4netTest));
            log.Debug("test log record");
        }
    }
}
