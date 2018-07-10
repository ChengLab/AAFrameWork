using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.AutoMapper
{
   public interface IMapperConfiguration
    {
        Action<IMapperConfigurationExpression> GetConfiguration();

        int Order { get; }
    }

}
