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

        private readonly IDapperContext _dapperContext;
        public UserInfoRepository(IDapperContext context) : base(context)
        {
            _dapperContext = context;
        }



    }
}
