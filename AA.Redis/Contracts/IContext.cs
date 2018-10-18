using AA.Redis.Contracts.Providers;
using System;
 

namespace AA.Redis.Contracts
{
    /// <summary>
    /// Interface for Context class containing the public APIs.
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// Gets the cache API.
        /// </summary>
        ICacheProvider Cache { get; }
        /// <summary>
        /// Gets the collection API.
        /// </summary>
        /// <value>The collections.</value>
        ICollectionProvider Collections { get; }

        /// <summary>
        /// Gets the serializer for this context.
        /// </summary>
        ISerializer GetSerializer();
    }
}