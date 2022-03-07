using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Dapper.Advanced
{
    public class DataSourceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var customAttrs = invocation.MethodInvocationTarget.GetCustomAttributes(true);
            foreach (var attr in customAttrs)
            {
                var dataSource = attr as DataSourceAttribute;
                if (dataSource != null)
                {
                    DbContextHolder.SetDbSourceMode(dataSource.DataSourceType);
                }
            }
            invocation.Proceed();
        }
    }
}
