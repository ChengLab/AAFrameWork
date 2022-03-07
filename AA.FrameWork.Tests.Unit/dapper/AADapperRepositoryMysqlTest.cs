using AA.Dapper;
using AA.Dapper.Advanced;
using AA.Dapper.Repositories;
using AA.FrameWork.Tests.Unit.dapper.Entity;
using AA.FrameWork.Tests.Unit.dapper.Init;
using AA.FrameWork.Tests.Unit.dapper.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xunit;

namespace AA.FrameWork.Tests.Unit.dapper
{
    public class AADapperRepositoryMysqlTest
    {
        /*
        AA.dapper 使用步骤
        
        
        1.定义领域model
         
        2.定义数据表映射关系 



         */
        //IDapperContext dapperContext = new AADapperContext(); 推荐使用DI
        public static IDapperContext dapperContext = new DapperContext();
        IUserRepository userRepository = new UserRepository(dapperContext);
        public AADapperRepositoryMysqlTest()
        {
            DbEntityMap.InitMapCfgs();
            //init datasourse
            IDbDatasource dbDatasource = new DbDataSource();
            dbDatasource.Init(new NameValueCollection()
            {
                ["aa.dataSource.master_demo.connectionString"] = "Data Source =localhost; Initial Catalog = demo;User ID = root; Password = cheng@123;",
                ["aa.dataSource.master_demo.provider"] = "MySql"

            });

            //dbDatasource.Init(new NameValueCollection()
            //{
            //    ["aa.dataSource.slave_AaCenter.connectionString"] = "Data Source =.; Initial Catalog = AaCenterS1;User ID = sa; Password = lee2018;",
            //    ["aa.dataSource.slave_AaCenter.provider"] = "SqlServer"
            //});


        }
        /// <summary>
        /// Insert
        /// </summary>
        [Fact]
        public void TextInsert()
        {
    
            var user = new User()
            {
                Name = "李四",
                Age = 30
            };
            var result = userRepository.Insert(user);
        }

        /// <summary>
        /// Get
        /// </summary>
        [Fact]
        public void TextGet()
        {
            try
            {
                var users = userRepository.GetAll();
            }
            catch (Exception ex) { }
          

            var b = 0;
          
        }



    }
}
