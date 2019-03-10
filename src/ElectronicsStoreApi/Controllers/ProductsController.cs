using ElectronicsStoreApi.DomainModels;
using ElectronicsStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _products;

        public ProductsController(IProductRepository productRepository)
        {
            _products = productRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = await _products.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            Product product = await _products.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            Product createdProduct = await _products.CreateProduct(product);

            return CreatedAtAction(
                nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] Product product)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _products.UpdateProduct(id, product);
                return NoContent();
            }
            catch (EntityNotFoundException<Product>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            await _products.DeleteProduct(id);
            return NoContent();
        }
    }
}
