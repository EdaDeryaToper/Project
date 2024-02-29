using Microsoft.EntityFrameworkCore;
using ProductService.Entities;

namespace ProductService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                SeedData(scope.ServiceProvider.GetRequiredService<ProjectDbContext>());

                //SeedData(scope.ServiceProvider.GetService<TepDbContext>());
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error initializing database: {ex.Message}");
            }

        }
        private static void SeedData(ProjectDbContext projectDbContext)
        {
            projectDbContext.Database.Migrate();
            try
            {
                if (projectDbContext.Products.Any())
                {
                    Console.WriteLine("Already have data- no need to seed");
                    return;
                }
                var products = new List<Product>()
                {new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Çay",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow.AddHours(2),
                        Brand = "Lipton",
                        Description = "Sallama çay",
                        ImageUrl = "image",
                        Price = 20,
                        Category = new Category()
                        {
                            Id = Guid.NewGuid(),
                            Name = "içecek",
                            SubCategories = new HashSet<SubCategory>()
                            {
                                new(){Id=Guid.NewGuid(), Name = "sıcak içecek"},
                                new(){Id=Guid.NewGuid(), Name = "soğuk içecek"}
                            }
                        }

                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Gömlek",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow.AddHours(6),
                        Brand = "Koton",
                        Description = "ipek kumaş",
                        ImageUrl = "image2",
                        Price = 100,
                        Category = new Category()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Giyim",
                            SubCategories = new HashSet<SubCategory>()
                            {
                                new(){Id=Guid.NewGuid(), Name = "Kadın giyim"},
                                new(){Id=Guid.NewGuid(), Name = "Erkek Giyim"}
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sneaker",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow.AddHours(3),
                        Brand = "Nike",
                        Description = "Hafif ve rahat spor ayakkabı",
                        ImageUrl = "image3",
                        Price = 1550,
                        Category = new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = "Ayakkabı",
                            SubCategories = new HashSet<SubCategory>
                            {
                                new() { Id = Guid.NewGuid(), Name = "Spor Ayakkabılar" },
                                new() { Id = Guid.NewGuid(), Name = "Klasik Ayakkabılar" }
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Küpe",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow.AddHours(4),
                        Brand = "Atasay",
                        Description = "Zarif altın küpe, pırlanta detaylar",
                        ImageUrl = "image4",
                        Price = 500,
                        Category = new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = "Takı",
                            SubCategories = new HashSet<SubCategory>
                            {
                                new() { Id = Guid.NewGuid(), Name = "Küpeler" },
                                new() { Id = Guid.NewGuid(), Name = "Kolyeler" }
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Android",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow.AddHours(5),
                        Brand = "Samsung",
                        Description = "Yüksek performanslı akıllı telefon",
                        ImageUrl = "image5",
                        Price = 1200,
                        Category = new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = "Elektronik",
                            SubCategories = new HashSet<SubCategory>
                            {
                                new() { Id = Guid.NewGuid(), Name = "Cep Telefonları" },
                                new() { Id = Guid.NewGuid(), Name = "Bilgisayarlar" }
                            }
                        }
                    }



                };


                projectDbContext.Products.AddRange(products);
                projectDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error during database migration or seeding: {ex.Message}");
                throw;
            }

        }
    }
}
