using AA.Dapper.FluentMap;
using AA.Dapper.FluentMap.Configuration;
using AA.Dapper.FluentMap.Dommel;
using System;
using System.Collections.Generic;

namespace AA.Dapper.Configuration
{
    public static class MapConfiguration
    {
        public static void Init(List<Action<FluentMapConfiguration>> configures)
        {
            FluentMapper.Initialize(config =>
            {
                foreach (var a in configures)
                {
                    a.Invoke(config);
                };
                config.ForDommel();
            });
        }
    }
}