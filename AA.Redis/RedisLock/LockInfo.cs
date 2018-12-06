using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Redis.RedisLock
{
  public  class LockInfo
    {
        public LockInfo(string resource, string val, TimeSpan validity)
        {
            this.Resource = resource;
            this.ResourceVal = val;
            this.Validity = validity;
        }

        public string Resource { get; }
        public string ResourceVal { get; }
        public TimeSpan Validity { get;  }
    }
}
