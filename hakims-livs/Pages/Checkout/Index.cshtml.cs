#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using hakims_livs.Data;
using hakims_livs.Models;

namespace hakims_livs.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        public OrderRow OrderRow { get; set; }
        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }    

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // ToDo: Load products from Localstorage

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // ToDo: Create a order 

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
