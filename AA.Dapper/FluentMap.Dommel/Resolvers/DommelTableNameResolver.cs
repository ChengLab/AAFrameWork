using System;
using AA.Dapper.FluentMap.Dommel.Mapping;
using AA.Dapper.FluentMap.Mapping;
using Dommel;

namespace AA.Dapper.FluentMap.Dommel.Resolvers
{
    /// <summary>
    /// Implements the <see cref="DommelMapper.ITableNameResolver"/> interface by using the configured mapping.
    /// </summary>
    public class DommelTableNameResolver : DommelMapper.ITableNameResolver
    {
        /// <inheritdoc />
        public string ResolveTableName(Type type)
        {
            IEntityMap entityMap;
            if (FluentMapper.EntityMaps.TryGetValue(type, out entityMap))
            {
                var mapping = entityMap as IDommelEntityMap;

                if (mapping != null)
                {
                    return mapping.TableName;
                }
            }

            return DommelMapper.Resolvers.Default.TableNameResolver.ResolveTableName(type);
        }
    }
}
