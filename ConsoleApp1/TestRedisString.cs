using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AA.Redis.UnitTest
{
    public class TestRedisString
    {
        [Fact]
        public void Test_Set()
        {
            var redisContent = TestRedisBase.GetRedisContext();
            var redisString = redisContent.Collections.GetRedisString("stringA");
            redisString.GetSet("aaa");
            
       
        }
    }
}
