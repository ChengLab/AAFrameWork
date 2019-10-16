using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.FrameWork.Domain
{
    /// <summary>
    /// 软删除
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
