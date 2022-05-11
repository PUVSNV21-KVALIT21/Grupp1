using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;

namespace hakims_livs.Controllers
{
    [Route("api/placeorder")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        [HttpPost]
        public ActionResult GetShoppingcartItems(Root root)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in root.ShoppingCart)
                {

                }
                return Content("OK");

                //return RedirectToPage("./Index");
            }

            return Content("Error");

        }
    }

    public class Root
    {
        public List<ShoppingCart> ShoppingCart { get; set; }
    }

    public class ShoppingCart
    {
        public int productID { get; set; }
    }
}
