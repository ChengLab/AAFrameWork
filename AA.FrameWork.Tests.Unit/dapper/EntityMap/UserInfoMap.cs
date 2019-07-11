using AA.Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
 

namespace AA.Dapper.Test
{
   public class UserInfoMap : DommelEntityMap<UserInfo>
    {
        public UserInfoMap()
        {
            ToTable("Sys_UserInfo");//映射具体的表名
            Map(p => p.SysNo).IsKey().IsIdentity();//指定主键 ,IsIdentity是否自增
            Map(p=>p.LastLoginDate).Ignore();
        }
    }
}
