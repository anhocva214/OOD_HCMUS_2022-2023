namespace financial_management_service.Core.Dtos
{
    public class CategoryDto
    {
    }
    public class CategoryResDto
    {
        public string? Name { get; set; }
        public CategoryResDto()
        {
            Name = String.Empty;
        }
    }

    public class AddCategoryReqDto
    {
        public string? Name { get; set; }
        public AddCategoryReqDto()
        {
            Name = String.Empty;
        }
    }


    public class UpdateCategoryReqDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public UpdateCategoryReqDto()
        {
            Id = String.Empty;
            Name = String.Empty;
        }
    }
}
