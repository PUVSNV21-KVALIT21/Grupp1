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

namespace hakims_livs.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly hakims_livs.Data.ApplicationDbContext _context;

        public DetailsModel(hakims_livs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        public Category Category { get; set; }
        public string Categories { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(m => m.ID == id);

            foreach(var cat in Product.Categories)
            {
                Categories += cat.Name + "  ";
            }

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
