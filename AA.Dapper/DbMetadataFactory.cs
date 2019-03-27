using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Dapper
{
    /// <summary>
    /// Base class for the DbMetadata Factory implementations  （DbMetadata 工厂实现基类）
    /// </summary>
    public abstract class DbMetadataFactory
    {
        /// <summary>
        /// Gets the supported provider names.
        /// </summary>
        /// <returns>The enumeration of the supported provider names</returns>
        public abstract IReadOnlyCollection<string> GetProviderNames();

        /// <summary>
        /// Gets the database metadata associated to the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>The metadata instance for the requested provider</returns>
        public abstract DbMetadata GetDbMetadata(string providerName);
    }
}
