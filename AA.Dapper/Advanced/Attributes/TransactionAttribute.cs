using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Dapper.Advanced
{
    /// <summary>
    /// 事务标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class TransactionAttribute : Attribute
    {
    }
}
