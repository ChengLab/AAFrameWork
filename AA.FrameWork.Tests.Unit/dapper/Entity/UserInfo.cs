using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Dapper.Test
{
   public class UserInfo
    {
        /// <summary>
        /// SysNo
        /// </summary>
        public Guid SysNo
        {
            get;
            set;
        }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// RealName
        /// </summary>
        public string RealName
        {
            get;
            set;
        }

        /// <summary>
        /// Pwd
        /// </summary>
        public string Pwd
        {
            get;
            set;
        }

        /// <summary>
        /// UserType
        /// </summary>
        public int UserType
        {
            get;
            set;
        }

        /// <summary>
        /// Status
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        /// Mobile
        /// </summary>
        public string Mobile
        {
            get;
            set;
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// QQ
        /// </summary>
        public string Qq
        {
            get;
            set;
        }

        /// <summary>
        /// SuperUser
        /// </summary>
        public bool SuperUser
        {
            get;
            set;
        }

        /// <summary>
        /// CreateDate
        /// </summary>
        public DateTime GmtCreate
        {
            get;
            set;
        }

        /// <summary>
        /// LastLoginDate
        /// </summary>
        public DateTime LastLoginDate
        {
            get;
            set;
        }

        /// <summary>
        /// TenantId
        /// </summary>
        public Guid TenantId
        {
            get;
            set;
        }

        public DateTime GmtModified { get; set; }

        public string Roles { get; set; }
    }
}
