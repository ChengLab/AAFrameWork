namespace AA.FrameWork.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a paged result.
    /// </summary>
    public interface IPagedResultRequestDto
    {
        /// <summary>
        /// Skip count (beginning of the page).
        /// </summary>
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}