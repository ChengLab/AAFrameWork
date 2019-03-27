 
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using AA.Dapper.Util;

namespace AA.Dapper
{
    public class DbProvider : IDbProvider
    {

        protected const string PropertyDbProvider = "quartz.dbprovider";
        //protected const string DbProviderSectionName = "quartz";
        protected const string DbProviderResourceName = "AA.Dapper.dbproviders.properties";


        private readonly MethodInfo commandBindByNamePropertySetter;

        private static readonly IList<DbMetadataFactory> dbMetadataFactories;
        private static readonly Dictionary<string, DbMetadata> dbMetadataLookup = new Dictionary<string, DbMetadata>();

        /// <summary>
        /// Parse metadata once in static constructor.
        /// </summary>
        static  DbProvider()
        {
            dbMetadataFactories = new List<DbMetadataFactory>
            {
                //new ConfigurationBasedDbMetadataFactory(DbProviderSectionName, PropertyDbProvider),
                new EmbeddedAssemblyResourceDbMetadataFactory(DbProviderResourceName, PropertyDbProvider)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbProvider"/> class.
        /// </summary>
        /// <param name="dbProviderName">Name of the db provider.</param>
        /// <param name="connectionString">The connection string.</param>
        public DbProvider(string dbProviderName, string connectionString)
        {
            ConnectionString = connectionString;
            Metadata = GetDbMetadata(dbProviderName);

            if (Metadata == null)
            {
                throw new ArgumentException($"Invalid DB provider name: {dbProviderName}{Environment.NewLine}{GenerateValidProviderNamesInfo()}");
            }

            // check if command supports direct setting of BindByName property, needed for Oracle Managed ODP diver at least
            var property = Metadata.CommandType.GetProperty("BindByName", BindingFlags.Instance | BindingFlags.Public);
            if (property != null && property.PropertyType == typeof(bool) && property.CanWrite)
            {
                commandBindByNamePropertySetter = property.GetSetMethod();
            }
        }

        public void Initialize()
        {
            // do nothing, initialized in static constructor
        }

        ///<summary>
        /// Registers DB metadata information for given provider name.
        ///</summary>
        ///<param name="dbProviderName"></param>
        ///<param name="metadata"></param>
        public static void RegisterDbMetadata(string dbProviderName, DbMetadata metadata)
        {
            dbMetadataLookup[dbProviderName] = metadata;
        }

        private DbMetadata GetDbMetadata(string providerName)
        {
            if (!dbMetadataLookup.TryGetValue(providerName, out var result))
            {
                foreach (var dbMetadataFactory in dbMetadataFactories)
                {
                    if (dbMetadataFactory.GetProviderNames().Contains(providerName))
                    {
                        result = dbMetadataFactory.GetDbMetadata(providerName);
                        RegisterDbMetadata(providerName, result);
                        return result;
                    }
                }
                throw new ArgumentOutOfRangeException(nameof(providerName), "There is no metadata information for provider '" + providerName + "'");
            }

            return result;
        }

        /// <summary>
        /// Generates the valid provider names information.
        /// </summary>
        /// <returns></returns>
        protected static string GenerateValidProviderNamesInfo()
        {
            var providerNames = dbMetadataFactories
                .SelectMany(factory => factory.GetProviderNames())
                .Distinct()
                .OrderBy(name => name);

            StringBuilder sb = new StringBuilder("Valid DB Provider names are:").Append(Environment.NewLine);
            foreach (string providerName in providerNames)
            {
                sb.Append("\t").Append(providerName).Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <inheritdoc />
        public virtual DbCommand CreateCommand()
        {
            var command = ObjectUtils.InstantiateType<DbCommand>(Metadata.CommandType);
            commandBindByNamePropertySetter?.Invoke(command, new object[] { Metadata.BindByName });
            return command;
        }

        /// <inheritdoc />
        public virtual DbConnection CreateConnection()
        {
            var conn = ObjectUtils.InstantiateType<DbConnection>(Metadata.ConnectionType);
            conn.ConnectionString = ConnectionString;
            return conn;
        }

        /// <summary>
        /// Returns a new parameter object for binding values to parameter
        /// placeholders in SQL statements or Stored Procedure variables.
        /// </summary>
        /// <returns>A new <see cref="IDbDataParameter"/></returns>
        public virtual DbParameter CreateParameter()
        {
            return ObjectUtils.InstantiateType<DbParameter>(Metadata.ParameterType);
        }

        /// <inheritdoc />
        public string ConnectionString { get; set; }

        /// <inheritdoc />
        public DbMetadata Metadata { get; }
    }
}
