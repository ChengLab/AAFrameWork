using System;
using System.Collections.Generic;
using System.Text;
using AA.FrameWork.Logging;
using log4net;
using log4net.Config;
namespace AA.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        public FrameWork.Logging.ILog Get(string name)
        {
            var logger = LogManager.GetLogger(System.Reflection.Assembly.GetEntryAssembly(), name);
            return new Log4NetLog(logger);
        }

        public void Shutdown()
        {
            LogManager.Shutdown();
        }


        public static void Use()
        {
            Logger.UseLogger(new Log4NetLogger());
        }
    }
}
