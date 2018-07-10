using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Logging
{
   public interface ILogger
    {
        ILog Get(string name);
        void Shutdown();
    }
}
