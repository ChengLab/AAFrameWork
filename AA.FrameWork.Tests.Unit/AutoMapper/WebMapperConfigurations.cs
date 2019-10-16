using AA.AutoMapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.AutoMapper
{
    public class WebMapperConfigurations : IMapperConfiguration
    {
        public int Order { get { return 0; } }

        public Action<IMapperConfigurationExpression>  GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<UserVm, UserInput>();
            };
            return action;
        }
    }
}
