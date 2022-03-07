using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AA.FrameWork;
namespace AA.Dapper.Advanced
{
    public static class DbContextHolder
    {
        private static AsyncLocal<string> asyncLocal = new AsyncLocal<string>();

        public static string _master = "master";

        public static string _slave = "slave";
        public static string GetDbSourceMode()
        {
            if (string.IsNullOrEmpty(asyncLocal.Value))
            {
                return _master;
            }
            return asyncLocal.Value;
        }

        public static void SetDbSourceMode(string dbType)
        {
            if (string.IsNullOrEmpty(dbType))
            {
                throw new AAException("DbContextHolder SetDbSourceMode set dbType not null");
            }
            asyncLocal.Value = dbType;
        }
 
    }
}
