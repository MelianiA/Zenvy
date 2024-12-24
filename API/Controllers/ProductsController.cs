using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used to access the products data.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {
        [HttpGet] // api/products
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            return Ok(await repo.GetProductsAsync());
        }

        [HttpGet("{id:int}")] // api/products/2
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpPost] // api/products
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.AddProduct(product);

            if (await repo.SaveChangesAsync())
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

            return BadRequest();
        }

        [HttpPut("{id:int}")] // api/products/2
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
                return BadRequest("Cannot update this product");

            repo.UpdateProduct(product); 

            if(await repo.SaveChangesAsync())   
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id:int}")] // api/products/2
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            repo.DeleteProduct(product);
            if(await repo.SaveChangesAsync())
                return NoContent();

            return BadRequest();
        }

        private bool ProductExists(int id)
        {
            return repo.ProductExists(id);
        }
    }
}
