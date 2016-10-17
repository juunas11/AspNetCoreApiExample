using ElectronicsStoreApi.DomainModels;
using ElectronicsStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetAllProducts()
        {
            List<Product> products = _products.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(long id)
        {
            Product product = _products.GetProduct(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            Product createdProduct = _products.CreateProduct(product);

            return CreatedAtAction(
                nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(long id, [FromBody] Product product)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _products.UpdateProduct(id, product);
                return Ok();
            }
            catch (EntityNotFoundException<Product>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(long id)
        {
            _products.DeleteProduct(id);
            return Ok();
        }
    }
}
