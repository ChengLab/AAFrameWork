using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AA.Redis.Util
{
    public class EmbeddedResourceLoader
    {
        /// <summary>
        /// 获取嵌入资源 luascript
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string GetEmbeddedResource(string name)
        {
            var assembly = typeof(EmbeddedResourceLoader).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(name))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
