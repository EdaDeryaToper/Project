namespace ProductService.Entities
{
    public class SearchParams
    {
        public string? SearchTerm { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public string? OrderBy { get; set; }
        public string? FilterBy { get; set; }
    }
}
