using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used to access the products data.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
        [HttpGet] // api/products
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var products = await repo.GetAllWithSpec(new ProductSpecification(productParams));
            return Ok(products);
        }

        [HttpGet("{id:int}")] // api/products/2
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpPost] // api/products
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);

            if (await repo.SaveAllAsync())
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

            return BadRequest();
        }

        [HttpPut("{id:int}")] // api/products/2
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
                return BadRequest("Cannot update this product");

            repo.Update(product); 

            if(await repo.SaveAllAsync())   
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id:int}")] // api/products/2
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            repo.Delete(product);
            if(await repo.SaveAllAsync())
                return NoContent();

            return BadRequest();
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }

        [HttpGet("types")] // api/products/types
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
        {
            return Ok(await repo.GetAllWithSpec(new TypeListSpecification()));
        }

        [HttpGet("brands")] // api/products/brands
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
        {
            return Ok(await repo.GetAllWithSpec(new BrandListSpecification()));
        }
    }
}
