using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;
using Microsoft.AspNetCore.Authorization;

namespace hakims_livs.Controllers
{
    [Route("api/placeorder")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private AccessControl _accessControl;
        private ApplicationDbContext _context;

        public ShoppingCartController(AccessControl accessControl, ApplicationDbContext context)
        {
            _accessControl = accessControl;
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> GetShoppingcartItems(Root root)
        {
            Customer currentUser = await _accessControl.GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Redirect("../Identity/Account/Login?ReturnUrl=%2FCheckout");
            }
            if (ModelState.IsValid)
            {

                var order = new Order
                {
                    Customer = currentUser,
                    OrderDate = DateTime.Now
                };

                foreach(var item in root.ShoppingCart)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ID == item.productID);
                    var orderRow = new OrderRow();
                    orderRow.Price = product.SalesPrice;
                    orderRow.Product = product;
                    orderRow.Quantity = item.quantity;
                    orderRow.Order = order;
                    order.OrderRows.Add(orderRow);
                }

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                
                return Redirect("../Orders/Details?id=" + order.ID);
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
        public int quantity { get; set; }
    }
}
