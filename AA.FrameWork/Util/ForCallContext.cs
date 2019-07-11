using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
#if NETFX
using System.Runtime.Remoting.Messaging;
#endif
namespace AA.FrameWork.Util
{
    public static class ForCallContext
    {
#if NETFX
#else
                static ConcurrentDictionary<string, AsyncLocal<object>> state = new ConcurrentDictionary<string, AsyncLocal<object>>();
#endif

        public static void SetData(string name, object data) =>
#if NETFX
       CallContext.LogicalSetData(name, data);
#else
                        state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

#endif

        public static object GetData(string name) =>
#if NETFX
            CallContext.LogicalGetData(name);
#else
               state.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;
#endif

    }
}
