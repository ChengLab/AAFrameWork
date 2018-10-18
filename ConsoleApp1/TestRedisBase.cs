using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Redis.UnitTest
{
    public class TestRedisBase
    {
        public static string Config = "192.168.109.129:6379,password=antifakeredis2018";

        public static RedisContext GetRedisContext()
        {
            return new RedisContext(configuration: Config);
        }
    }
}
