using AA.Dapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace AA.FrameWork.Tests.Unit.dapper
{
    public class AADapperContext : DapperContext
    {
        public AADapperContext() : base(new NameValueCollection()
        {
            ["aa.dataSource.AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenter;User ID = sa; Password = 123;",
            ["aa.dataSource.AaCenter.provider"] = "SqlServer"
        })
        { }
    }
}
