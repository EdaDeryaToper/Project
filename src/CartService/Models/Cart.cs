namespace CartService.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CartName { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
    }
}
