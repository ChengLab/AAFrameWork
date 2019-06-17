using AA.Dapper.Configuration;
using AA.Dapper.FluentMap.Configuration;
using AA.Dapper.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.dapper.Init
{
   public class DbEntityMap
    {
        public static void Map()
        {
            Action<FluentMapConfiguration> mps = x =>
        {
            x.AddMap(new UserInfoMap());

        };
            var fluentMapconfig = new List<Action<FluentMapConfiguration>>();
            fluentMapconfig.Add(mps);
            MapConfiguration.Init(fluentMapconfig);
        }
    }
}
