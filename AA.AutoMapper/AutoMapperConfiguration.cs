using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;

        public static void Init(List<Action<IMapperConfigurationExpression>> configurationActions)
        {
            if (configurationActions == null)
                throw new ArgumentNullException("configurationActions");

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                foreach (var ca in configurationActions)
                {
                    ca(cfg);
                }
            });
            _mapper = _mapperConfiguration.CreateMapper();
        }
        public static MapperConfiguration MapperConfiguration
        {
            get { return _mapperConfiguration; }
        }

        public static IMapper Mapper => _mapper;
    }
}
