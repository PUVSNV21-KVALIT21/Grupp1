using hakims_livs.Models;
using hakims_livs.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace hakims_livs.Data;

public class DbInitializer
{
    public static void Initialize(ApplicationDbContext context, IWebHostEnvironment environment)
    {
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }
            
            // Copy example data images to wwwRoot folder
            var destinationDir = environment.WebRootPath + "/images/";
            var sourceDir = "./Images";

            ClearDirectory(destinationDir);
            CopyDirectory(sourceDir, destinationDir);

            // Seed data
            var products = new Product[] {
                new Product
                {
                    Name= "Clementin",
                    Description= "God citrus", 
                    Image = "clementine.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 15, 
                    CreatedDateTime= DateTime.Parse("2019-11-12"),
                    Origin = "Frankrike"
                },
                new Product
                {
                    Name= "Äpple",
                    Description= "Royal Gala", 
                    Image = "apple.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 33, 
                    CreatedDateTime= DateTime.Parse("2021-09-01"),
                    Origin = "Skåne"
                    
                },
                new Product
                {
                    Name= "Körsbär",
                    Description= "Allas favorit", 
                    Image = "cherry.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 30,
                    Stock = 500,
                    CreatedDateTime= DateTime.Parse("2020-09-01"),
                    Origin = "Spanien"
                },
                new Product
                {
                    Name= "Ananas",
                    Description= "Tropisk Frukt", 
                    Image = "pineapple.jpg", 
                    Volume = 1,
                    Unit = Unit.st, SalesPrice = 59, 
                    Stock = 500,
                    CreatedDateTime= DateTime.Parse("2019-10-01"),
                    Origin = "Costa Rica"
                },
                new Product
                {
                    Name= "Jordgubbe",
                    Description= "Sommarklassiker", 
                    Image = "strawberry.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, SalesPrice = 25, 
                    CreatedDateTime= DateTime.Parse("2019-10-01")
                },
                new Product
                {
                    Name= "Päron",
                    Description= "Höstfrukt", 
                    Image = "pear.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, SalesPrice = 25, 
                    CreatedDateTime= DateTime.Parse("2019-10-01")
                },
                new Product
                {
                    Name= "Citron",
                    Description= "Sur och gul", 
                    Image = "lemon.jpg", 
                    Volume = 1, 
                    Unit = Unit.st, SalesPrice = 15, 
                    CreatedDateTime= DateTime.Parse("2021-10-21")
                },
                new Product
                {
                    Name= "Fikon",
                    Description= "Kort Hållbarhet", 
                    Image = "fig.jpg", 
                    Volume = 2, 
                    Unit = Unit.st, SalesPrice = 20, 
                    CreatedDateTime= DateTime.Parse("2021-11-21")
                },
                new Product
                {
                    Name= "Ingefära",
                    Description= "Knölig rot", 
                    Image = "ginger.jpg", 
                    Volume = 500, 
                    Unit = Unit.gram, SalesPrice = 39, 
                    CreatedDateTime= DateTime.Parse("2021-11-21")
                },
                new Product
                {
                    Name= "Avocado",
                    Description= "Extra pris!", 
                    Image = "avocado.jpg", 
                    Volume = 500, 
                    Unit = Unit.st, SalesPrice = 10, 
                    CreatedDateTime= DateTime.Parse("2021-11-21")
                },
                new Product
                {
                    Name= "Vindruvor",
                    Description= "Kärnfria", 
                    Image = "grapes.jpg", 
                    Volume = 500, 
                    Unit = Unit.st, SalesPrice = 29, 
                    CreatedDateTime= DateTime.Parse("2021-12-21")
                },
                new Product
                {
                    Name= "Bananer",
                    Description= "Fullproppade med vitaminer", 
                    Image = "bananas.jpg", 
                    Volume = 1000, 
                    Unit = Unit.gram, SalesPrice = 19, 
                    CreatedDateTime= DateTime.Parse("2021-04-21")
                },
            };


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
}