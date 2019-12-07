using AA.FrameWork.Logging;
using AA.NLogging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AA.FrameWork.Tests.Unit.NLog
{
   public class NLogTest
    {
        [Fact]
        public void TestNLog()
        {
            NLogLogger.Use("NLog/nlog.config");
            ILog log = Logger.Get(typeof(NLogTest));
            log.Debug("test nlog record");
        }
    }
}
