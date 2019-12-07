using AA.FrameWork.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.NLogging
{
    public class NLogLog : ILog
    {

        private readonly NLog.Logger _log;

        public NLogLog(NLog.Logger log)
        {
            _log = log;
        }

       public void Debug(object obj)
        {
            _log.Log(NLog.LogLevel.Debug, obj);
        }

        public void Debug(object obj, Exception exception)
        {
            _log.Log(NLog.LogLevel.Debug, exception, obj?.ToString() ?? "");
        }


        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Debug, formatProvider, format, args);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Debug, format, args);
        }

        public void Error(object obj)
        {
            _log.Log(NLog.LogLevel.Error, obj);
        }

        public void Error(object obj, Exception exception)
        {
            _log.Log(NLog.LogLevel.Error, exception, obj?.ToString() ?? "");
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Error, formatProvider, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Error, format, args);
        }

        public void Fatal(object obj)
        {
            _log.Log(NLog.LogLevel.Fatal, obj);
        }

        public void Fatal(object obj, Exception exception)
        {
            _log.Log(NLog.LogLevel.Fatal, exception, obj?.ToString() ?? "");
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Fatal, formatProvider, format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Fatal, format, args);
        }

        public void Info(object obj)
        {
            _log.Log(NLog.LogLevel.Info, obj);
        }

        public void Info(object obj, Exception exception)
        {
            _log.Log(NLog.LogLevel.Info, exception, obj?.ToString() ?? "");
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Warn, formatProvider, format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Info, format, args);
        }

        public void Warn(object obj)
        {
            _log.Log(NLog.LogLevel.Warn, obj);
        }

        public void Warn(object obj, Exception exception)
        {
            _log.Log(NLog.LogLevel.Warn, exception, obj?.ToString() ?? "");
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Warn, formatProvider, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.Log(NLog.LogLevel.Warn, format, args);
        }

        public bool IsDebugEnabled => _log.IsDebugEnabled;

        public bool IsInfoEnabled => _log.IsInfoEnabled;

        public bool IsWarnEnabled => _log.IsWarnEnabled;

        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public bool IsFatalEnabled => _log.IsFatalEnabled;
    }
}
