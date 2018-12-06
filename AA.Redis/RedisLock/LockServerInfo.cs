using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;

namespace AA.Redis.RedisLock
{
    public class LockServerInfo
    {

        public LockServerInfo()
        {
            EndPoints = new List<EndPoint>();
        }
        /// <summary>
        /// 单个endpoint
        /// </summary>
        /// <param name="endPoint"></param>
        public LockServerInfo(EndPoint endPoint)
        {
            this.EndPoint = endPoint;
        }
        /// <summary>
        /// endpoint 列表
        /// 用于连接集群
        /// </summary>
        /// <param name="endPoints"></param>
        public LockServerInfo(IList<EndPoint> endPoints)
        {
            this.EndPoints = endPoints ?? new List<EndPoint>();
        }

        /// <summary>
        /// redis连接 使用endpoint
        /// </summary>
        public EndPoint EndPoint
        {
            get
            {
                return EndPoints.FirstOrDefault();
            }
            set
            {
                EndPoints = new List<EndPoint> { value };
            }
        }

        /// <summary>
        /// redis连接使用EndPoint .集群
        /// </summary>
        public IList<EndPoint> EndPoints
        {
            get; private set;
        }

        /// <summary>
        /// redis连接密码
        /// </summary>
        public string Password { get; set; }

    }
}
