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

namespace hakims_livs.Pages.Products
{
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
            var imageChanged = Request.Form.Files.Count > 0;
            if (imageChanged)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();

                var name = Product.Name ?? "";
                var path = await FileStorage.Store(file, name);
                Product.Image = path;
            }

            _context.Attach(Product).State = EntityState.Modified;
            if (!imageChanged) _context.Entry(Product).Property(x => x.Image).IsModified=false; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
