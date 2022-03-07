using AA.Dapper.FluentMap.Dommel.Mapping;
using AA.FrameWork.Tests.Unit.dapper.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.dapper.EntityMap
{
   public class UserMap : DommelEntityMap<User>
    {
        public UserMap()
        {
            ToTable("user");//映射具体的表名
            Map(p => p.Id).IsKey().IsIdentity();//指定主键 ,IsIdentity是否自增         
        }
    }
}
