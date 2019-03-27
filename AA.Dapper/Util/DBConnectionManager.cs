using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Dapper.Util
{
   public class DBConnectionManager : IDbConnectionManager
    {
        private static readonly DBConnectionManager instance = new DBConnectionManager();
        //private static readonly ILog log = LogProvider.GetLogger(typeof(DBConnectionManager));

        private readonly object syncRoot = new object();
        private readonly Dictionary<string, IDbProvider> providers = new Dictionary<string, IDbProvider>();

        /// <summary>
		/// Get the class instance.
		/// </summary>
		/// <returns> an instance of this class
		/// </returns>
		public static IDbConnectionManager Instance => instance;

        /// <summary>
		/// Private constructor
		/// </summary>
		private DBConnectionManager()
        {
        }

        /// <summary>
        /// Adds the connection provider.
        /// </summary>
        /// <param name="dataSourceName">Name of the data source.</param>
        /// <param name="provider">The provider.</param>
        public virtual void AddConnectionProvider(string dataSourceName, IDbProvider provider)
        {
            //log.Info($"Registering datasource '{dataSourceName}' with db provider: '{provider}'");

            lock (syncRoot)
            {
                providers[dataSourceName] = provider;
            }
        }

        /// <summary>
        /// Get a database connection from the DataSource with the given name.
        /// </summary>
        /// <returns> a database connection </returns>
        public virtual DbConnection GetConnection(string dataSourceName)
        {
            var provider = GetDbProvider(dataSourceName);
            return provider.CreateConnection();
        }

        /// <summary>
		/// Shuts down database connections from the DataSource with the given name,
		/// if applicable for the underlying provider.
		/// </summary>
		public virtual void Shutdown(string dsName)
        {
            IDbProvider provider = GetDbProvider(dsName);
           // provider.Shutdown();
        }

        public DbMetadata GetDbMetadata(string dsName)
        {
            return GetDbProvider(dsName).Metadata;
        }

        /// <summary>
        /// Gets the db provider.
        /// </summary>
        /// <param name="dsName">Name of the ds.</param>
        /// <returns></returns>
	    public IDbProvider GetDbProvider(string dsName)
        {
            if (string.IsNullOrEmpty(dsName))
            {
                throw new ArgumentException("DataSource name cannot be null or empty", nameof(dsName));
            }

            IDbProvider provider;
            lock (syncRoot)
            {
                providers.TryGetValue(dsName, out provider);
            }

            if (provider == null)
            {
                throw new Exception($"There is no DataSource named '{dsName}'");
            }

            return provider;
        }
    }
}
