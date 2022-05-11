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

namespace hakims_livs.Pages
{
    public class IndexModel : PageModel
    {
        private readonly hakims_livs.Data.ApplicationDbContext _context;

        public IndexModel(hakims_livs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }
        public IList<Category> Category { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
           // Product = await _context.Products.ToListAsync();
            var products = from product in _context.Products select product;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Name.Contains(SearchString));
            }

            Product = await products.ToListAsync();

            Category = await _context.Categories.ToListAsync();
        }

        
    }
}

