using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.FrameWork.Domain
{
    public class PageRequest
    {
        /// <summary>
        /// current page number
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// page of size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// id asc,name dec
        /// </summary>
        public string OrderFiled { get; set; }
        /// <summary>
        /// sql
        /// </summary>
        public string SqlText { get; set; }
        /// <summary>
        /// sql参数 参照dapper {} 
        /// </summary>
       public  object SqlParam { get; set; }
    }
}
