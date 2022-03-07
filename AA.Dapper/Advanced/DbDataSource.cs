using AA.Dapper.Util;
using AA.FrameWork;
using AA.FrameWork.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace AA.Dapper.Advanced
{
    public class DbDataSource : IDbDatasource
    {
        public const string PropertyDataSourcePrefix = "aa.dataSource";
        public const string PropertyDataSourceProvider = "provider";
        public const string PropertyDataSourceConnectionString = "connectionString";

        private static readonly  DbDataSource dbDataSource = new DbDataSource();

        private AAException initException;
        private PropertiesParser cfg;

        public static IDbDatasource Instance => dbDataSource;
        public void Init(NameValueCollection props)
        {
            IDbConnectionManager dbMgr = null;
            cfg = new PropertiesParser(props);

            var dsNames = cfg.GetPropertyGroups(PropertyDataSourcePrefix);
            foreach (string dataSourceName in dsNames)
            {
                string datasourceKey = "{0}.{1}".FormatInvariant(PropertyDataSourcePrefix, dataSourceName);
                NameValueCollection propertyGroup = cfg.GetPropertyGroup(datasourceKey, true);
                PropertiesParser pp = new PropertiesParser(propertyGroup);
                string dsProvider = pp.GetStringProperty(PropertyDataSourceProvider, null);
                string dsConnectionString = pp.GetStringProperty(PropertyDataSourceConnectionString, null);

                if (dsProvider == null)
                {
                    initException = new AAException("Provider not specified for DataSource: {0}".FormatInvariant(dataSourceName));
                    throw initException;
                }
                if (dsConnectionString == null)
                {
                    initException = new AAException("Connection string not specified for DataSource: {0}".FormatInvariant(dataSourceName));
                    throw initException;
                }
                try
                {
                    DbProvider dbp = new DbProvider(dsProvider, dsConnectionString);
                    dbp.Initialize();

                    dbMgr = DBConnectionManager.Instance;
                    dbMgr.AddConnectionProvider(dataSourceName, dbp);

                }
                catch (Exception exception)
                {
                    initException = new AAException("Could not Initialize DataSource: {0}".FormatInvariant(dataSourceName), exception);
                    throw initException;
                }
            }
        }
    }
}
