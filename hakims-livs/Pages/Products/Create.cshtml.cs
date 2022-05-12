#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using hakims_livs.Data;
using hakims_livs.Models;
using hakims_livs.Utils;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace hakims_livs.Pages.Products
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            CategoryOptions = new SelectList(GetCategories(), nameof(Category.ID), nameof(Category.Name));

            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        public byte[] Image { get; set; }

        [BindProperty]
        public int[] SelectedCategories { get; set; }
        public SelectList CategoryOptions { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Image")]
            public byte[] Image { get; set; }
        }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newProduct = new Product();
            
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();

                var name = Product.Name ?? "";
                var path = await FileStorage.StoreFileAsync(file, name);
                newProduct.Image = path;
            }

            if (SelectedCategories.Length > 0)
            {
                foreach (var cat in SelectedCategories)
                {
                    var category = _context.Categories.Where(c => c.ID == cat).FirstOrDefault();
                    newProduct.Categories.Add(category);
                }
            }

            newProduct.Name = Product.Name;
            newProduct.Description = Product.Description;
            newProduct.SalesPrice = Product.SalesPrice;
            newProduct.Stock = Product.Stock;
            newProduct.Volume = Product.Volume;
            newProduct.Unit = Product.Unit;
            newProduct.IsVegan = Product.IsVegan;
            newProduct.IsGluten = Product.IsGluten;
            newProduct.IsEco = Product.IsEco;
            newProduct.ExpiryDate = Product.ExpiryDate;
            newProduct.Brand = Product.Brand;
            newProduct.Origin = Product.Origin;

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        internal IEnumerable GetCategories()
        {
            return _context.Categories;
        }
    }
}
