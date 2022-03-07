﻿using AA.Dapper.Configuration;
using AA.Dapper.FluentMap.Configuration;
using AA.Dapper.Test;
using AA.FrameWork.Tests.Unit.dapper.EntityMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.dapper.Init
{
    public class DbEntityMap
    {
        public static void InitMapCfgs()
        {
            var fluentMapconfig = new List<Action<FluentMapConfiguration>>();
            fluentMapconfig.Add(cfg =>
            {
                cfg.AddMap(new UserInfoMap());
                cfg.AddMap(new VillageMap());
                cfg.AddMap(new UserMap());
            });
            MapConfiguration.Init(fluentMapconfig);
        }
    }
}
