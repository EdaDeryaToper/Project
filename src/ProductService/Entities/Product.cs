﻿namespace ProductService.Entities
{
    public class Product:BaseEntity
    {
        
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
       // public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
