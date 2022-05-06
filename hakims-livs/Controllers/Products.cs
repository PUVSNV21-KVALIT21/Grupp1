#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;

namespace hakims_livs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Products(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/ProductDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
