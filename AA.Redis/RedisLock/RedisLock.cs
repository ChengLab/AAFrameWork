using AA.Redis.Util;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AA.Redis.RedisLock
{
    public class RedisLock
    {

        private readonly IList<LockServerInfo> lockServerInfos;
        private readonly int RetryCount = 3;
        private readonly TimeSpan RetryDelay = new TimeSpan(0, 0, 0, 0, 400);
        private readonly double ClockDriveFactor = 0.01;
        protected readonly int Quorum;

        private static readonly string UnlockScript = EmbeddedResourceLoader.GetEmbeddedResource("AA.Redis.Lua.Unlock.lua");
        public string LockResourceVal { get; set; }
        public RedisLock(IList<LockServerInfo> lockServerInfos)
        {
            this.lockServerInfos = lockServerInfos;
            this.Quorum = lockServerInfos.Count / 2 + 1;
            this.LockResourceVal = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 获取锁
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="ttl"></param>
        /// <param name="lockObject"></param>
        /// <returns></returns>
        public bool TryAcquireLock(string resourceId, TimeSpan ttl, out LockInfo lockObject)
        {
            LockInfo innerLock = null;
            bool isAcquired = true;
            isAcquired = ReTryRedisLock(RetryCount, RetryDelay, () =>
              {
                  try
                  {
                      int n = 0;
                      var startTime = DateTime.Now;

                      // Use keys
                      ForEachRedisRegistered(
                          redis =>
                          {
                              if (LockInstance(redis, resourceId, LockResourceVal, ttl)) n += 1;
                          }
                      );

                      /*
                       * Add 2 milliseconds to the drift to account for Redis expires
                       * precision, which is 1 millisecond, plus 1 millisecond min drift 
                       * for small TTLs.        
                       */
                      var drift = Convert.ToInt32((ttl.TotalMilliseconds * ClockDriveFactor) + 2);
                      var validity_time = ttl - (DateTime.Now - startTime) - new TimeSpan(0, 0, 0, 0, drift);

                      if (n >= Quorum && validity_time.TotalMilliseconds > 0)
                      {
                          innerLock = new LockInfo(resourceId, LockResourceVal, validity_time);
                          return true;
                      }
                      else
                      {
                          ForEachRedisRegistered(
                              redis =>
                              {
                                  UnlockInstance(redis, resourceId, LockResourceVal);
                              }
                          );
                          return false;
                      }
                  }
                  catch (Exception)
                  { return false; }
              });
            lockObject = innerLock;
            return isAcquired;
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="lockInfo"></param>
        public void ReleaseLock(LockInfo lockInfo)
        {
            ForEachRedisRegistered(redis =>
            {
                UnlockInstance(redis, lockInfo.Resource, lockInfo.ResourceVal);
            });
        }
        protected void ForEachRedisRegistered(Action<IConnectionMultiplexer> action)
        {
            foreach (var item in lockServerInfos)
            {
                var conf = new ConfigurationOptions()
                {
                    Password = item.Password,
                };
                foreach (var e in item.EndPoints)
                {
                    conf.EndPoints.Add(e);
                }
                var redis = new RedisContext(conf);
                action(redis.GetConnectionMultiplexer());
            }

        }

        private bool LockInstance(IConnectionMultiplexer connection, string resourceId, string resourceVal, TimeSpan ttl)
        {
            bool succeeded;
            try
            {
                var db = connection.GetDatabase();
                succeeded = db.StringSet(resourceId, resourceVal, ttl, When.NotExists);
            }
            catch (Exception ex)
            {
                succeeded = false;
            }
            return succeeded;
        }
        protected void UnlockInstance(IConnectionMultiplexer connection, string resourceId, string resourceVal)
        {
            connection.GetDatabase().ScriptEvaluate(UnlockScript, new RedisKey[] { resourceId }, new RedisValue[] { resourceVal });
        }
        protected bool ReTryRedisLock(int retryCount, TimeSpan retryDelay, Func<bool> func)
        {
            int maxRetryDelay = (int)retryDelay.TotalMilliseconds;
            Random rnd = new Random();
            int currentRetry = 0;

            while (currentRetry++ < retryCount)
            {
                if (func()) return true;
                Thread.Sleep(rnd.Next(maxRetryDelay));
            }
            return false;
        }

    }
}
