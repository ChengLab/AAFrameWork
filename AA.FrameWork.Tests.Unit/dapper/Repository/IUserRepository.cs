using AA.Dapper.Repositories;
using AA.FrameWork.Tests.Unit.dapper.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.dapper.Repository
{
   public interface IUserRepository : IDapperRepository<User>
    {
    }
}
