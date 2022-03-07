using AA.Dapper.FluentMap.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Dapper.Configuration
{
    public interface IMapConfiguration
    {
        public Action<FluentMapConfiguration> GetConfiguration();
        
    }
}
