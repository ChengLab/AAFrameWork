using AA.FrameWork.ObjectMapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using IObjectMapper = AA.FrameWork.ObjectMapping.IObjectMapper;
namespace AA.AutoMapper
{
    public class AutoMapperObjectMapper : IObjectMapper
    {
        private readonly IMapper _mapper;

        public AutoMapperObjectMapper()
        {
            _mapper = AutoMapperConfiguration.Mapper;
        }
        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
