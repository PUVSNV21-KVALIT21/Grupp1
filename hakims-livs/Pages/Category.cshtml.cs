#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;

namespace hakims_livs.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }
        public List<Product> Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string searchString = "")
        {
            if (id == null)
            {
                return NotFound();
            }


            Category = await _context.Categories.Include(c => c.Products).ThenInclude(p => p.Categories).FirstOrDefaultAsync(m => m.ID == id);
            Products = Category.Products.Where(product => product.Name.ToLower().Contains(searchString.ToLower()) && product.Stock > 0).ToList();


            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
