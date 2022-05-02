using hakims_livs.Models;
using hakims_livs.Utils;

namespace hakims_livs.Data;

public class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }
            
            var products = new Product[] {
                new Product
                {
                    Name= "Clementin",
                    Description= "God citrus", 
                    Image = "apple.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, Price = 15, 
                    CreatedDateTime= DateTime.Parse("2019-09-01")
                    
                },
                new Product{Name= "Ananas",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-08-01")},
                new Product{Name= "Jordubbe",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-07-01")},
                new Product{Name= "Citron",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Körsbär",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},
                new Product{Name= "Clementiner",Description= "God citrus", Volume = 500, Unit = Unit.gram, Price = 15, CreatedDateTime= DateTime.Parse("2019-09-01")},

            };

            context.Products.AddRange(products);
            context.SaveChanges();

        }
}