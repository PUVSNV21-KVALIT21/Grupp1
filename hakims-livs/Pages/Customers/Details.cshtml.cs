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

namespace hakims_livs.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public Customer toBeRemoved { get; set; }

        public List<Order> orders { get; set; }
        public List<OrderRow> orderRows { get; set; }


        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("admin"))
            {
                return Forbid();
            }

            Customer = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(string? id)
        {
            Customer = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(m => m.Id == id);

            orders = await _context.Orders
                .Where(order => order.Customer.Id == Customer.Id)
                .Include(order => order.OrderRows)
                .ToListAsync();

            _context.Remove(Customer);
            _context.RemoveRange(orders);  
            await _context.SaveChangesAsync();
            return RedirectToPage("Customers");
        }
    }
}
