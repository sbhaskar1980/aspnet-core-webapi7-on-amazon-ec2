using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.API.Models;

namespace ProductCRUD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDBContext _dbContext;

        public ProductsController(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product is null) { return NotFound(id); }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductRequest request)
        {
            var product = Product.Create(request.name, request.description, request.price);
            await _dbContext.Products.AddAsync(product);
            _dbContext.SaveChanges();
            return Ok(product.Id);
        }
    }
}
