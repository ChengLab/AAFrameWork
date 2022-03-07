using AA.Dapper;
using AA.Dapper.Repositories;
using AA.FrameWork.Tests.Unit.dapper.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.dapper.Repository
{
   public class UserRepository:DapperRepository<User>, IUserRepository
    {
        public IDapperContext dapperContext;
        public UserRepository(IDapperContext dapperContext) : base(dapperContext)
        {
            this.dapperContext = dapperContext;
        }
    }
}
