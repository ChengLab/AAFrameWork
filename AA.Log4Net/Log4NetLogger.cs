using System;
using System.Collections.Generic;
using System.IO;
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
#if NETCORE
            var logger = LogManager.GetLogger(System.Reflection.Assembly.GetEntryAssembly(), name);
#else
            var logger = LogManager.GetLogger(name);
#endif
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

        public static void Use(string file)
        {
            Logger.UseLogger(new Log4NetLogger());
#if NETCORE
            file = Path.Combine(AppContext.BaseDirectory, file);
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(file));

#else
            file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            XmlConfigurator.Configure(new FileInfo(file));
#endif
        }
    }
}
