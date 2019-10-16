#if NETFX
#else
using Microsoft.Extensions.Configuration;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AA.FrameWork.Util
{
    public static class ConfigUtil
    {
#if NETFX
#else

        private static IConfiguration _configuration;

        static ConfigUtil()
        {
            var fileName = "appsettings.json";

            var directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            var filePath = $"{directory}/{fileName}";
            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true);

            _configuration = builder.Build();
        }

        public static string GetSectionValue(string key)
        {
            return _configuration.GetSection(key).Value;
        }

#endif
    }
}
