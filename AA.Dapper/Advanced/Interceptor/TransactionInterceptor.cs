using AA.FrameWork.Engine;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Dapper.Advanced
{
    public class TransactionInterceptor : IInterceptor
    {
        private IDapperContext dapperContext;
        public void Intercept(IInvocation invocation)
        {
            var customAttrs = invocation.MethodInvocationTarget.GetCustomAttributes(true);
            foreach (var attr in customAttrs)
            {
                var transactionAttribute = attr as TransactionAttribute;
                if (transactionAttribute != null)
                {
                    //事务操作默认在主库上进行
                    DbContextHolder.SetDbSourceMode("master");
                    dapperContext = EngineContext.Current.Resolve<IDapperContext>();
                    using (var dbtransaction = dapperContext.BeginTransaction())
                    {
                        try
                        {
                            invocation.Proceed();
                            dbtransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                            dbtransaction.Rollback();
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            invocation.Proceed();
        }
    }
}
