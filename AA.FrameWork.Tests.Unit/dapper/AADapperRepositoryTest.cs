using AA.Dapper;
using AA.Dapper.Advanced;
using AA.Dapper.Repositories;
using AA.Dapper.Test;
using AA.FrameWork.Tests.Unit.dapper.Init;
using AADemo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xunit;

namespace AA.FrameWork.Tests.Unit.dapper
{

    public class AADapperRepositoryTest
    {
        //IDapperContext dapperContext = new AADapperContext(); 推荐使用DI
        public static IDapperContext dapperContext = new DapperContext();
        IDapperRepository<UserInfo> userInfoRepository = new DapperRepository<UserInfo>(dapperContext);
        IVillageRepository villageRepository = new VillageRepository(dapperContext);
        public AADapperRepositoryTest()
        {
            DbEntityMap.InitMapCfgs();
            //init datasourse
            IDbDatasource dbDatasource = new DbDataSource();
            dbDatasource.Init(new NameValueCollection()
            {
                ["aa.dataSource.master_AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenter;User ID = sa; Password = db123;",
                ["aa.dataSource.master_AaCenter.provider"] = "SqlServer"

            });

            //dbDatasource.Init(new NameValueCollection()
            //{
            //    ["aa.dataSource.slave_AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenterS1;User ID = sa; Password = db123;",
            //    ["aa.dataSource.slave_AaCenter.provider"] = "SqlServer"
            //});


        }
        /// <summary>
        /// Insert
        /// </summary>
        [Fact]
        public void TextInsert()
        {

            var user = new UserInfo()
            {
                UserName = "chengTian",
                RealName = "成天",
                GmtCreate = DateTime.Now,
                GmtModified = DateTime.Now,
                LastLoginDate=DateTime.Now
            };
            var result = userInfoRepository.Insert(user);

           
        }
        /// <summary>
        /// Update
        /// </summary>
        [Fact]
        public void TextUpdate()
        {
            var user = userInfoRepository.Get(1);
            user.GmtModified = DateTime.Now;
            var result = userInfoRepository.Update(user);
        }
        /// <summary>
        /// Remove
        /// </summary>
        [Fact]
        public void TextRemove()
        {
            var user = userInfoRepository.Get(1);
            var result = userInfoRepository.Delete(user);
        }
        /// <summary>
        /// Get
        /// </summary>
        [Fact]
        public void TextGet()
        {
            var user = userInfoRepository.Get(1);
            var users = userInfoRepository.GetAll();
        }
        /// <summary>
        /// Select
        /// </summary>
        [Fact]
        public void TextSelect()
        {
            var users = userInfoRepository.Select(p => p.UserName == "chengTian");
        }
        /// <summary>
        /// Transaction
        /// </summary>
        [Fact]
        public void TestTran()
        {
            try
            {
                dapperContext.BeginTransaction();
                var user = userInfoRepository.Get(5);
                var model = villageRepository.Get(new Guid("6D880321-DB17-4B32-9F0A-CE9F3F25AA01"));
                model.VillageName = "ccccccccccccccc";
                villageRepository.Update(model);

                user.GmtModified = DateTime.Now;
                user.UserName = "aaaaaaaaaaaaaa";
                var result = userInfoRepository.Update(user);
                dapperContext.Commit();
            }
            catch (Exception ex)
            {
                dapperContext.RollBack();
            }
        }

    }
}
