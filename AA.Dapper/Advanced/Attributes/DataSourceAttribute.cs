using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Dapper.Advanced
{
    /// <summary>
    /// 数据源类型 master slave
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class DataSourceAttribute:Attribute
    {
        private string _DataSourceType;
        public DataSourceAttribute(string dsType)
        {
            _DataSourceType = dsType;
        }

        public string DataSourceType
        {
            get { return _DataSourceType; }
            set { _DataSourceType = value; }
        }
    }
}
