#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;
using hakims_livs.Utils;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace hakims_livs.Pages.Products
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public InputModel Input { get; set; }
        public byte[] Image { get; set; }
        [BindProperty]
        public int[] SelectedCategories { get; set; }
        public SelectList CategoryOptions { get; set; }
        public class InputModel
        {
            [Display(Name = "Bild")]
            public byte[] Image { get; set; }

        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);

            CategoryOptions = new SelectList(GetCategories(), nameof(Category.ID), nameof(Category.Name));

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbProduct = _context.Products.Include(p => p.Categories).Where(p => p.ID == Product.ID).FirstOrDefault();
            var previousImagePath = dbProduct.Image;

            if (!ProductExists(Product.ID))
            {
                return NotFound();
            }


            if (dbProduct == null)
            {
                return NotFound();
            }

            if (SelectedCategories.Length > 0)
            {
                foreach (var dbCat in dbProduct.Categories.ToList())
                {
                    dbProduct.Categories.Remove(dbCat);
                }

                foreach (var cat in SelectedCategories)
                {
                    var category = _context.Categories.Where(c => c.ID == cat).FirstOrDefault();
                    dbProduct.Categories.Add(category);
                }
            }

            var imageChanged = Request.Form.Files.Count > 0;

            if (imageChanged)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                var name = Product.Name ?? "";
                if (file != null)
                {
                    var path = await FileStorage.StoreFileAsync(file, name);
                    if (previousImagePath != null) FileStorage.Delete(previousImagePath);

                    dbProduct.Image = path;
                }
            }
            else
            {
                dbProduct.Image = await _context.Products.Where(p => p.ID == Product.ID).Select(p => p.Image).FirstOrDefaultAsync();
            }

            dbProduct.Name = Product.Name;
            dbProduct.Description = Product.Description;
            dbProduct.SalesPrice = Product.SalesPrice;
            dbProduct.Stock = Product.Stock;
            dbProduct.Volume = Product.Volume;
            dbProduct.Unit = Product.Unit;
            dbProduct.IsVegan = Product.IsVegan;
            dbProduct.IsGluten = Product.IsGluten;
            dbProduct.IsEco = Product.IsEco;
            dbProduct.ExpiryDate = Product.ExpiryDate;
            dbProduct.Brand = Product.Brand;
            dbProduct.Origin = Product.Origin;

            //_context.Attach(Product).State = EntityState.Modified;
            //if (!imageChanged) _context.Entry(Product).Property(x => x.Image).IsModified=false;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        internal IEnumerable GetCategories()
        {
            return _context.Categories;
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
