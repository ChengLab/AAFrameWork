using AA.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleApp1
{
   
   public class TestConnectionMultiplexer
    {
        [Fact]
        public void Test_CustomMultiplexer()
        {
            using (var ctx = new RedisContext("192.168.109.129:6379,password=antifakeredis2018"))
            {
                ctx.Cache.SetObject("Test_CustomMultiplexer_obj", "Test_CustomMultiplexer_value");
                var list = ctx.Collections.GetRedisDictionary<string, string>("Test_CustomMultiplexer_hash");
                list.Add("test", "value");
            }

            using (var ctx = new RedisContext("192.168.109.129:6379,password=antifakeredis2018"))
            {
                Assert.Equal("Test_CustomMultiplexer_value", ctx.Cache.GetObject<string>("Test_CustomMultiplexer_obj"));
                var dict = ctx.Collections.GetRedisDictionary<string, string>("Test_CustomMultiplexer_hash");
                Assert.Equal("value", dict["test"]);
                ctx.Cache.Remove("Test_CustomMultiplexer_obj");
                ctx.Cache.Remove("Test_CustomMultiplexer_hash");
            }
        }
    }
}
