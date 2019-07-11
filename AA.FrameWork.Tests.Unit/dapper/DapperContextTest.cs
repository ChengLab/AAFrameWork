using AA.Dapper;
using AA.FrameWork.Tests.Unit.dapper.Init;
using AADemo.DataAccess.Repository;
using AADemo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xunit;

namespace AA.FrameWork.Tests.Unit.dapper
{
    public class DapperContextTest
    {
        [Fact]
        public void TextDapperContext()
        {
            DbEntityMap.InitMapCfgs();
            IDapperContext dapperContext = new DapperContext(new NameValueCollection()
            {
                ["aa.dataSource.AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenter;User ID = sa; Password = 123;",
                ["aa.dataSource.AaCenter.provider"] = "SqlServer"
            });
            IUserInfoRepository _userInforepository = new UserInfoRepository();
            var users = _userInforepository.QueryAll();
        }


    }

   

}
