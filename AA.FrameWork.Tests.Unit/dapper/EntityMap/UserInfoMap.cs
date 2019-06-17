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
           ToTable("Sys_UserInfo");
            Map(p => p.SysNo).IsKey();
        }
    }
}
