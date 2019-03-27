using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Dapper.Util
{
   internal static class Configuration
    {
    

        internal static NameValueCollection GetSection(string sectionName)
        {
            try
            {
                return (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            }
            catch (Exception e)
            {
               // log.Warn("could not read configuration using ConfigurationManager.GetSection: " + e.Message);
                return null;
            }
        }
    }
}
