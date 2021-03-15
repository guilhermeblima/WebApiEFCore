using System.Collections.Generic;
using System.Threading.Tasks;
using ApiEFCore.Data;
using ApiEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiEFCore.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(p => p.Category) // populate the Category field on return
            .AsNoTracking() //Stop from creating proxies - improves performance
            .FirstOrDefaultAsync(p => p.Id == id);
            return products;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products
                .Include(p => p.Category) // populate the Category field on return
                .AsNoTracking() //Stop from creating proxies - improves performance
                .Where(p => p.CategoryId == id)
                .ToListAsync(); // Always last to avoid prod issues
            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post(
            [FromServices] DataContext context,
            [FromBody] Product model)
        {
            if(ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}