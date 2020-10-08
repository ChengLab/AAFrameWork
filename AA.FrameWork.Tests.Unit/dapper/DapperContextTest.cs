using AA.Dapper;
using AA.Dapper.Test;
using AA.FrameWork.Tests.Unit.dapper.Init;
using AADemo.DataAccess.Repository;
using AADemo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using Xunit;
using System.Linq.Expressions;
using AA.Dapper.Util;

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
                ["aa.dataSource.AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenter;User ID = sa; Password = lee2018;",
                ["aa.dataSource.AaCenter.provider"] = "SqlServer"
            });
            IUserInfoRepository _userInforepository = new UserInfoRepository();
            IVillageRepository villageRepository = new VillageRepository();
            villageRepository.Insert(new Village { 
              Id=Guid.NewGuid(),
              VillageName="aa",
              GmtCreate=DateTime.Now,
              GmtModified=DateTime.Now
            });


            var model = villageRepository.Get(new Guid("6D880321-DB17-4B32-9F0A-CE9F3F25AA01"));
            model.VillageName = "bbb";
            villageRepository.Update(model);

            //var obj = _userInforepository.Insert(new UserInfo()
            //{
            //    RealName = "111",
            //    UserName = "bbb",
            //    GmtCreate=DateTime.Now,
            //    LastLoginDate=DateTime.Now,
            //    GmtModified=DateTime.Now
            //});
            var users = _userInforepository.QueryAll();
            var userList = _userInforepository.From(sql =>
                sql.Select()
                .Where(p => p.RealName.Contains("成"))
                .OrderBy(x => x.SysNo)
                .Page(1, 20)
               );

            var count = userList.ToList().Count();

            //动态where
            Expression<Func<UserInfo, bool>> expression = p => p.RealName == "成天";
            var where = DynamicWhereExpression.Init<UserInfo>();

            where = where.And(x => x.RealName == "成天");
            var dynamicUsers = _userInforepository.From(sql =>
                sql.Select()
                .Where(where)
                .OrderBy(x => x.SysNo)
                .Page(1, 20)
            );
            var count2 = dynamicUsers.ToList().Count();
        }


    }



}
