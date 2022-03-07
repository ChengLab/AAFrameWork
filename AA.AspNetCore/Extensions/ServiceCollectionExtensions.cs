using AA.AutoMapper;
using AA.Dapper;
using AA.Dapper.Advanced;
using AA.Dapper.Configuration;
using AA.Dapper.FluentMap.Configuration;
using AA.FrameWork.Engine;
using AA.FrameWork.ObjectMapping;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AA.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAa(this IServiceCollection services, NameValueCollection dbInfo)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            EngineContext.Create();

            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            var binPath = AppContext.BaseDirectory;
            string[] dllPaths = Directory.GetFiles(binPath, "*.dll", SearchOption.TopDirectoryOnly);

            var loadedAssemblyNames = new List<string>();

            foreach (var a in assemblys)
            {
                loadedAssemblyNames.Add(a.FullName);
            }
            foreach (var dllPath in dllPaths)
            {
                try
                {
                    var an = AssemblyName.GetAssemblyName(dllPath);
                    if (!loadedAssemblyNames.Contains(an.FullName))
                    {
                        AppDomain.CurrentDomain.Load(an);
                    }
                }
                catch (BadImageFormatException ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }
            assemblys = AppDomain.CurrentDomain.GetAssemblies();
            //automapper
            var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            foreach (var assembly in assemblys)
            {
                var instancesMapper = assembly.GetTypes().Where(x => x.GetInterface("IMapperConfiguration") != null)
                    .Select(mapper => (IMapperConfiguration)Activator.CreateInstance(mapper));

                foreach (var instance in instancesMapper)
                {
                    configurationActions.Add(instance.GetConfiguration());
                }
            }
            AutoMapperConfiguration.Init(configurationActions);
            ObjectMapManager.ObjectMapper = new AutoMapperObjectMapper();
            //AA.Dapper
            Action<FluentMapConfiguration> action = null;
            foreach (var assembly in assemblys)
            {
                var instancesMapper = assembly.GetTypes().Where(x => x.GetInterface("IMapConfiguration") != null)
                    .Select(mapper => (IMapConfiguration)Activator.CreateInstance(mapper));

                foreach (var instance in instancesMapper)
                {
                    action = instance.GetConfiguration();
                }
            }
            IDbDatasource dbDatasource = new DbDataSource();
            dbDatasource.Init(dbInfo);
            services.AddScoped<IDapperContext, DapperContext>();

            if (action == null)
            {
                throw new Exception("FluentMapConfiguration is null");
            }
            else
            {
                MapConfiguration.Init(action);
            }


        }

        public static void AddInterceptor(this ContainerBuilder builder, out Type[] types)
        {
            builder.RegisterType<TransactionInterceptor>().InstancePerLifetimeScope();
            builder.RegisterType<DataSourceInterceptor>().InstancePerLifetimeScope();

            types = new Type[] { typeof(TransactionInterceptor), typeof(DataSourceInterceptor) };
        }


    }
}
