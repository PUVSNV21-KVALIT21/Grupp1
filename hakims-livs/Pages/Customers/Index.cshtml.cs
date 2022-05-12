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
using Microsoft.AspNetCore.Identity;

namespace hakims_livs.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customers { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("admin"))
            {
                return Forbid();
            }

            Customers = await _context.Users
                .Include(u => u.Address)
                .Where(u => u.UserName != "admin@gmail.com")
                .OrderBy(u => u.FirstName)
                .ToListAsync();

            return Page();
        }
    }
}
