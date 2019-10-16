using AA.AutoMapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AA.FrameWork.Extensions;
namespace AA.FrameWork.Tests.Unit.AutoMapper
{

    public class AutoMapperTest
    {
        private void Init()
        {
            var mapperConfig = new WebMapperConfigurations();
            AutoMapperConfiguration.Init(new List<Action<IMapperConfigurationExpression>> { mapperConfig.GetConfiguration() });
            ObjectMapping.ObjectMapManager.ObjectMapper = new AutoMapperObjectMapper();
        }

        [Fact]
        public void TestMap()
        {
            //init
            Init();
            UserVm userVm = new UserVm { Id = 1, Name = "成天" ,Remark="微信公众号:dotNet知音"};
            var userDto = userVm.MapTo<UserInput>();
            //var userDto2 = userVm.MapTo<UserVm,UserInput>();

        }


    }
}
