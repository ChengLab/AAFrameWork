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
using AA.Dapper.Advanced;
using System.Threading;
using System.Threading.Tasks;

namespace AA.FrameWork.Tests.Unit.dapper
{
    public class DapperContextTest
    {
        //public static IDapperContext dapperContext = new DapperContext();

        [Fact]
        public void TextDapperContext()
        {
            //TODO 主从 数据库使用下划线_作为分隔符
            DbEntityMap.InitMapCfgs();
            //init datasourse
            IDbDatasource dbDatasource = new DbDataSource();
            dbDatasource.Init(new NameValueCollection()
            {
                ["aa.dataSource.master_AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenter;User ID = sa; Password = db123;",
                ["aa.dataSource.master_AaCenter.provider"] = "SqlServer"

            });

            dbDatasource.Init(new NameValueCollection()
            {
                ["aa.dataSource.slave_AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenterS1;User ID = sa; Password = db123;",
                ["aa.dataSource.slave_AaCenter.provider"] = "SqlServer"
            });




            IDapperContext dapperContext = new DapperContext();
            IDapperContext dapperContextSlave = new DapperContext();
            DbContextHolder.SetDbSourceMode("master");
            IUserInfoRepository _userInforepository = new UserInfoRepository(dapperContext);
            IVillageRepository villageRepository = new VillageRepository(dapperContext);
            villageRepository.Insert(new Village
            {
                Id = Guid.NewGuid(),
                VillageName = "aa",
                GmtCreate = DateTime.Now,
                GmtModified = DateTime.Now
            });

            DbContextHolder.SetDbSourceMode("slave");
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

        [Fact]
        public void TextDapperContextMul()
        {
            //TODO 主从 数据库使用下划线_作为分隔符
            DbEntityMap.InitMapCfgs();
            //init datasourse
            IDbDatasource dbDatasource = new DbDataSource();
            dbDatasource.Init(new NameValueCollection()
            {
                ["aa.dataSource.master_AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenter;User ID = sa; Password = db123;",
                ["aa.dataSource.master_AaCenter.provider"] = "SqlServer"

            });

            dbDatasource.Init(new NameValueCollection()
            {
                ["aa.dataSource.slave_AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenterS1;User ID = sa; Password = db123;",
                ["aa.dataSource.slave_AaCenter.provider"] = "SqlServer"
            });





            IDapperContext dapperContext = new DapperContext();


            IVillageRepository villageRepository = new VillageRepository(dapperContext);

            IUserInfoRepository userInfoRepository = new UserInfoRepository(dapperContext);

            DbContextHolder.SetDbSourceMode("slave");

            try
            {
                var user = userInfoRepository.Count();    
                var model = villageRepository.Get(new Guid("6D880321-DB17-4B32-9F0A-CE9F3F25AA01"));

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }



}
