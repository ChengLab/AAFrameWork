using AA.AutoMapper;
using AA.Testing.Mapper.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Testing.Mapper.MaperCfg
{
    public class TestMapperConfiguration : IMapperConfiguration
    {

        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<StartDto, StartVm>();
            };

            return action;
        }

        public int Order
        {
            get { return 0; }

        }


    }
}
