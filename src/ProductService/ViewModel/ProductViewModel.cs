namespace ProductService.ViewModel
{
    public class ProductViewModel
    {
        public string ProductId { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; } 
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string User { get; set; }
        public List<SubCategoryViewModel> SubCategories { get; set; }
    }
}
