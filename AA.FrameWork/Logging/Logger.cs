using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Logging
{
    public static class Logger
    {
        static ILogger _logger;
        public static ILogger Current => _logger;

        public static ILog Get(Type type)
        {
            return Get(type.FullName);
        }

        public static ILog Get(string name)
        {
            return Current.Get(name);
        }

        public static void UseLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void Shutdown()
        {
            _logger?.Shutdown();
        }

    }
}
