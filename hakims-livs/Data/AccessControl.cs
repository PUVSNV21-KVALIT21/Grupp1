using hakims_livs.Models;
using Microsoft.AspNetCore.Identity;

namespace hakims_livs.Data
{
    public class AccessControl
    {
        public string LoggedInUserID { get; private set; }

        public AccessControl(UserManager<Customer> userManager, IHttpContextAccessor httpContextAccessor)
        {
            LoggedInUserID = userManager.GetUserId(httpContextAccessor.HttpContext.User);
        }

        public bool UserCanAccess(Customer customer)
        {
            return customer.Id == LoggedInUserID;
        }

    }
}
