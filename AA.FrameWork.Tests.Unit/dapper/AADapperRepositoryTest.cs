using AA.Dapper;
using AA.Dapper.Repositories;
using AA.Dapper.Test;
using AA.FrameWork.Tests.Unit.dapper.Init;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AA.FrameWork.Tests.Unit.dapper
{

    public class AADapperRepositoryTest
    {
        IDapperContext dapperContext = new AADapperContext();
        IDapperRepository<UserInfo> userInfoRepository = new DapperRepository<UserInfo>();
        public AADapperRepositoryTest()
        {
            DbEntityMap.InitMapCfgs();
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
                GmtModified = DateTime.Now
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
            dapperContext.BeginTransaction();
            dapperContext.Commit();
        }

    }
}
