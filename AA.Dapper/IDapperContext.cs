using AA.Dapper.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AA.Dapper
{
    public interface IDapperContext : IDisposable
    {
        IDbConnection Connection { get; }
        IDbConnection GetCurrentConnection();
        IDbTransaction GetCurrentTransaction();
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead);
        void Commit();
        void RollBack();
         DataBase DataBase { get; }

    }
}
