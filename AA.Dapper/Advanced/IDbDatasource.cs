using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace AA.Dapper.Advanced
{
   public interface IDbDatasource
    {
        void Init(NameValueCollection props);
    }
}
