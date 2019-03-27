using Dapper.FluentMap;
using Dapper.FluentMap.Configuration;
using Dapper.FluentMap.Dommel;
using System;
using System.Collections.Generic;
using System.Text;

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
