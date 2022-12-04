namespace financial_management_service.Core.Dtos
{
    public class PagingReqDto
    {
        public string? Keyword { get; set; } 
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public PagingReqDto()
        {
            Keyword = string.Empty;
            PageSize = 1;
            PageIndex = 1;
        }
    }
}
