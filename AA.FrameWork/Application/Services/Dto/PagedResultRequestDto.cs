using System;
 

namespace AA.FrameWork.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedResultRequestDto : IPagedResultRequestDto
    {
        public virtual int PageIndex { get; set; }
        public virtual int PageSize { get; set; }
    }
}