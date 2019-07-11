using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.AspNetCore.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        int Order { get; }
    }
}
