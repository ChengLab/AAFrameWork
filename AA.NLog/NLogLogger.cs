using AA.FrameWork.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AA.NLogging
{
    public class NLogLogger : FrameWork.Logging.ILogger
    {
        private readonly bool _shutdownLogManager;

        public NLogLogger()
        {
            _shutdownLogManager = true;
        }
        public ILog Get(string name)
        {
            var logger = LogManager.GetLogger(name);
            return new NLogLog(logger);
        }

        public void Shutdown()
        {
            LogManager.Flush();

            if (_shutdownLogManager)
                LogManager.Shutdown();
        }

        public static void Use(string file)
        {
            FrameWork.Logging.Logger.UseLogger(new NLogLogger());
#if NETCORE
            file = Path.Combine(AppContext.BaseDirectory, file);
#else
            file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
#endif
            LogManager.LoadConfiguration(file);
        }
    }
}
