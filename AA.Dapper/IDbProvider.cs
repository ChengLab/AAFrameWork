using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Dapper
{
   public interface IDbProvider
    {
        void Initialize();

        /// <summary>
        /// Returns a new connection object to communicate with the database.
        /// </summary>
        /// <returns>A new <see cref="IDbConnection"/></returns>
        DbConnection CreateConnection();

        /// <summary>
        /// Connection string used to create connections.
        /// </summary>
        string ConnectionString { set; get; }

        DbMetadata Metadata { get; }

    }
}
