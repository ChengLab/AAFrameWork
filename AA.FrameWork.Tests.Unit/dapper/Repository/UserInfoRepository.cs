using AA.Dapper;
using AA.Dapper.Repositories;
using AA.Dapper.Test;
using AADemo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AADemo.DataAccess.Repository
{
    public class UserInfoRepository : DapperRepository<UserInfo>, IUserInfoRepository
    {

        public IDapperContext dapperContext;
        public UserInfoRepository(IDapperContext dapperContext) : base(dapperContext)
        {
            this.dapperContext = dapperContext;
        }
        public IEnumerable<UserInfo> QueryAll()
        {
            var result = dapperContext.DataBase.Query<UserInfo>("SELECT * from  [Sys_UserInfo]");
            return result;
        }
    }
}
