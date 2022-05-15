using hakims_livs.Models;
using hakims_livs.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace hakims_livs.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context, IWebHostEnvironment environment)
    {
            // InitializeCustomers(context, userManager);
            
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }


            // Copy example data images to wwwRoot folder
            var destinationDir = environment.WebRootPath + "/images/";
            var sourceDir = "./Images";

            ClearDirectory(destinationDir);
            CopyDirectory(sourceDir, destinationDir);
            var categories = new List<Category>
            {
                new Category() {Name = "Frukt & Grönt"},
                new Category() {Name = "Mejeriprodukter"},
                new Category() {Name = "Kött & Fisk"},
                new Category() {Name = "Godis"},
                new Category() {Name = "Dricka"},
                new Category() {Name = "Tvätt & Städ"}
            };
            
            // Seed data
            var products = new Product[] {
                new Product
                {
                    Name= "Clementin",
                    Description= "God citrus", 
                    Image = "clementine.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 15, 
                    Stock = 250,
                    CreatedDateTime= DateTime.Parse("2019-11-12"),
                    Origin = "Frankrike",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Äpple",
                    Description= "Royal Gala", 
                    Image = "apple.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 33, 
                    Stock = 300,
                    CreatedDateTime= DateTime.Parse("2021-09-01"),
                    Origin = "Skåne",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                    
                },
                new Product
                {
                    Name= "Körsbär",
                    Description= "Allas favorit", 
                    Image = "cherry.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 30,
                    Stock = 1250,
                    CreatedDateTime= DateTime.Parse("2020-09-01"),
                    Origin = "Spanien",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Ananas",
                    Description= "Tropisk Frukt", 
                    Image = "pineapple.jpg", 
                    Volume = 1,
                    Unit = Unit.st, SalesPrice = 59, 
                    Stock = 35,
                    CreatedDateTime= DateTime.Parse("2019-10-01"),
                    Origin = "Costa Rica",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Jordgubbar",
                    Description= "Sommarklassiker", 
                    Image = "strawberry.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, SalesPrice = 500,
                    Stock = 23,
                    CreatedDateTime= DateTime.Parse("2019-10-01"),
                    Origin = "Sverige",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                    IsEco = true
                },
                new Product
                {
                    Name= "Päron",
                    Description= "Höstfrukt", 
                    Image = "pear.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = (decimal) 39.90, 
                    Stock = 455,
                    CreatedDateTime= DateTime.Parse("2019-10-01"),
                    Origin = "Sverige",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Citron",
                    Description= "Sur och gul", 
                    Image = "lemon.jpg", 
                    Volume = 1, 
                    Unit = Unit.st, SalesPrice = 15, 
                    Stock = 233,
                    CreatedDateTime= DateTime.Parse("2021-10-21"),
                    Origin = "Argentina",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Fikon",
                    Description= "Kort Hållbarhet", 
                    Image = "fig.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, SalesPrice = (decimal) 19.90, 
                    Stock = 12,
                    CreatedDateTime= DateTime.Parse("2021-11-21"),
                    Origin = "Turkiet",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                    IsEco = true
                },
                new Product
                {
                    Name= "Ingefära",
                    Description= "Knölig rot", 
                    Image = "ginger.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, SalesPrice = 45,
                    Stock = 11,
                    CreatedDateTime= DateTime.Parse("2021-11-21"),
                    Origin = "Sri Lanka",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Avocado",
                    Description= "Extra pris!", 
                    Image = "avocado.jpg", 
                    Volume = 2, 
                    Unit = Unit.st, SalesPrice = (decimal)44.50, 
                    Stock = 3,
                    CreatedDateTime= DateTime.Parse("2021-11-21"),
                    Origin = "Mexiko",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                    IsEco = true
                },
                new Product
                {
                    Name= "Vindruvor",
                    Description= "Kärnfria", 
                    Image = "grapes.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, SalesPrice = 29, 
                    Stock = 23,
                    CreatedDateTime= DateTime.Parse("2021-12-21"),
                    Origin = "Frankrike",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Bananer",
                    Description= "Fullproppade med vitaminer", 
                    Image = "bananas.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 19, 
                    Stock = 353,
                    CreatedDateTime= DateTime.Parse("2021-04-21"),
                    Origin = "Colombia",
                    Categories = new List<Category>{categories[0]},
                    IsVegan = true,
                    IsGluten = true,
                },
                new Product
                {
                    Name= "Mjölk",
                    Description= "Fetthalt 3.5%", 
                    Image = "milk.jpg",
                    Brand = "Arla",
                    Volume = 1000, 
                    Unit = Unit.ml, SalesPrice = 19, 
                    Stock = 48,
                    CreatedDateTime= DateTime.Parse("2021-04-21"),
                    Origin = "Sverige",
                    Categories = new List<Category>{categories[1]},
                    IsGluten = true,
                    IsEco = true
                },
            };

            context.Categories.AddRange(categories);
            context.Products.AddRange(products);
            context.SaveChanges();
        }

    private static void CopyDirectory(string sourceDir, string destinationDir)
    {
        var dir = new DirectoryInfo(sourceDir);

        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");


        if (!Directory.Exists(destinationDir)) Directory.CreateDirectory(destinationDir);
        



        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }
    }

    private static void ClearDirectory(string destinationDir)
    {
        var destDir = new DirectoryInfo(destinationDir);
        foreach (FileInfo file in destDir.GetFiles())
        {
            file.Delete();
        }
    }

   /// <summary>
   /// Adds customers to the database
   /// </summary>
   /// <param name="context"></param>
   /// <param name="userManager"></param>
   /// <param name="numberOfCustomers">Number of customers to add</param>
   /// <returns></returns>
    public static async Task CreateUser(ApplicationDbContext context, UserManager<Customer> userManager, int numberOfCustomers)
    {
        if (context.Users.Count() > 1) // Only Admin exists in Customer Db
        {
            return;
        }

        for (int i = 0; i < numberOfCustomers; i++)
        {
            Faker _faker = new Faker();
            var firstName = _faker.Name.FirstName();
            var lastName = _faker.Name.LastName();
            var email = firstName + "." + lastName + "@example.com";

            var user = new Customer
            {
                UserName = email,
                NormalizedUserName = email,
                Email = email,
                NormalizedEmail = email,
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = _faker.Phone.PhoneNumber("####-## ## ##"),
                Address = new Address
                {
                    Street = _faker.Address.StreetName() + " " + _faker.Random.Number(1, 99),
                    PostalCode = _faker.Address.ZipCode("### ##"),
                    City = _faker.Address.City(),
                    Country = _faker.Address.Country()
                }
            };
            await userManager.CreateAsync(user, "Test123!");
            await context.SaveChangesAsync();
        }
    }
}