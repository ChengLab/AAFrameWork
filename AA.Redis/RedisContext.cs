using AA.Redis.Contracts;
using AA.Redis.Contracts.Providers;
using AA.Redis.Providers;
using AA.Redis.Serializers;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AA.Redis
{
    /// <summary>
    /// Context class containing the public APIs.
    /// </summary>
    public class RedisContext: IContext,IDisposable
    {
        public static ISerializer DefaultSerializer { get; set; } = new JsonSerializer();

        #region Fields
        private readonly RedisProviderContext _internalContext;
        /// <summary>
        /// The cache provider
        /// </summary>
        private readonly ICacheProvider _cacheProvider;
        /// <summary>
        /// The collection provider
        /// </summary>
        private readonly ICollectionProvider _collectionProvider;

        #endregion


        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class using Redis in localhost server default port 6379, and using the default Serializer.
        /// </summary>
        public RedisContext() : this("localhost:6379") { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class given the cache engine type and its configuration string, and using the default Serializer.
        /// </summary>
        /// <param name="configuration">The configuration string.</param>
        public RedisContext(string configuration) : this(configuration, RedisContext.DefaultSerializer, null) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class given the cache engine type and its configuration string, and using the default Serializer.
        /// </summary>
        /// <param name="configuration">The configuration object.</param>
        public RedisContext(ConfigurationOptions configuration) : this(configuration, RedisContext.DefaultSerializer, null) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class.
        /// </summary>
        /// <param name="configuration">The configuration string.</param>
        /// <param name="serializer">The serializer.</param>
        public RedisContext(string configuration, ISerializer serializer) : this(configuration, serializer, null) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class.
        /// </summary>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="serializer">The serializer.</param>
        public RedisContext(ConfigurationOptions configuration, ISerializer serializer) : this(configuration, serializer, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class injecting the connection multiplexer and serializer to use.
        /// </summary>
        /// <param name="connection">The connection multiplexer to use.</param>
        /// <param name="serializer">The serializer.</param>
        public RedisContext(IConnectionMultiplexer connection, ISerializer serializer)
        {
            _internalContext = new RedisProviderContext(connection, serializer ?? RedisContext.DefaultSerializer);
            _cacheProvider = new RedisCacheProvider(_internalContext);
            _collectionProvider = new RedisCollectionProvider(_internalContext, _cacheProvider);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext"/> class injecting the connection multiplexer to use using the default serializer.
        /// </summary>
        /// <param name="connection">The connection multiplexer to use.</param>
        public RedisContext(IConnectionMultiplexer connection)
        {
            _internalContext = new RedisProviderContext(connection, RedisContext.DefaultSerializer);
            _cacheProvider = new RedisCacheProvider(_internalContext);
            _collectionProvider = new RedisCollectionProvider(_internalContext, _cacheProvider);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class.
        /// </summary>
        /// <param name="configuration">The configuration string.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="log">The textwriter to use for logging purposes.</param>
        public RedisContext(string configuration, ISerializer serializer, TextWriter log)
        {
            _internalContext = new RedisProviderContext(configuration, serializer ?? RedisContext.DefaultSerializer, log);
            _cacheProvider = new RedisCacheProvider(_internalContext);
            _collectionProvider = new RedisCollectionProvider(_internalContext, _cacheProvider);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisContext" /> class.
        /// </summary>
        /// <param name="configuration">The configuration object.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="log">The textwriter to use for logging purposes.</param>
        public RedisContext(ConfigurationOptions configuration, ISerializer serializer, TextWriter log)
        {
            _internalContext = new RedisProviderContext(configuration, serializer ?? RedisContext.DefaultSerializer, log);
            _cacheProvider = new RedisCacheProvider(_internalContext);
            _collectionProvider = new RedisCollectionProvider(_internalContext, _cacheProvider);
        }
        #endregion

        #region IContext implementation
        /// <summary>
        /// Gets the cache API.
        /// </summary>
        public ICacheProvider Cache
        {
            get { return _cacheProvider; }
        }
        /// <summary>
        /// Gets the collection API.
        /// </summary>
        /// <value>The collections.</value>
        public ICollectionProvider Collections
        {
            get { return _collectionProvider; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the StackExchange.Redis's connection multiplexer.
        /// Use this if you want to directly access the SE.Redis API.
        /// </summary>
        /// <returns>IConnectionMultiplexer.</returns>
        public IConnectionMultiplexer GetConnectionMultiplexer()
        {
            return _internalContext.RedisConnection;
        }

        /// <summary>
        /// Gets the serializer for this context.
        /// </summary>
        public ISerializer GetSerializer()
        {
            return _internalContext.Serializer;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            _internalContext.RedisConnection.Dispose();
        }
        #endregion
    }
}
