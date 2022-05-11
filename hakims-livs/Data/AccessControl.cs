using hakims_livs.Models;
using Microsoft.AspNetCore.Identity;

namespace hakims_livs.Data
{
    public class AccessControl
    {
        public string LoggedInUserID { get; private set; }
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public AccessControl(UserManager<Customer> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            LoggedInUserID = userManager.GetUserId(httpContextAccessor.HttpContext?.User);
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool UserCanAccess(Customer customer)
        {
            return customer.Id == LoggedInUserID;
        }

        public async Task<Customer> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        }

    }
}
