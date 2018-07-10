using AA.AutoMapper;
using AA.Testing.Mapper.MaperCfg;
using AA.Testing.Mapper.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AA.Testing.Mapper.Extensions;
namespace AA.Testing.Mapper.Run
{
   public class RunAAMapper
    {
        [Fact]
        public static void Tmap()
        {
            //注册 
            //获取对象
            var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            var testMapperConfiguration = new TestMapperConfiguration();
            configurationActions.Add(testMapperConfiguration.GetConfiguration());
            AutoMapperConfiguration.Init(configurationActions);
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
            StartDto startDto = new StartDto {
                Id=1,
                Name="lmc"

            };
            StartVm aa = new StartVm();
            startDto.MapTo(aa);
          

           
        }


        
    }
}
