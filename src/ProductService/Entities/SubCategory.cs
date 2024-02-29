namespace ProductService.Entities
{
    public class SubCategory:BaseEntity
    {
        public string Name { get; set; }
       // public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
