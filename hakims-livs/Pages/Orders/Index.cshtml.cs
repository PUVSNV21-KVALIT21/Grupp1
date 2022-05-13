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

namespace hakims_livs.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly AccessControl _accessControl;

        public IndexModel(ApplicationDbContext context, AccessControl accessControl)
        {
            _context = context;
            _accessControl = accessControl;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            var customer = await _accessControl.GetCurrentUserAsync();

            if(customer.Email == "admin@gmail.com")
            {
                Order = await _context.Orders.Include(o => o.Customer).ToListAsync();
            }
            else
            {
                Order = await _context.Orders.Include(o => o.Customer).Where(o => o.Customer == customer).ToListAsync();

            }
        }
    }
}
