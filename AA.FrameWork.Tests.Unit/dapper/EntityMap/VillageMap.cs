using AA.Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
namespace AA.Dapper.Test
{
   public class VillageMap:DommelEntityMap<Village>
    {
        public VillageMap()
        {
            ToTable("Village");
            Map(x=>x.Id).IsKey().ToColumn("Id");
        }
    }
}

