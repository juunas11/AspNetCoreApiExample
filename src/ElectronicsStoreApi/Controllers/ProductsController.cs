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
    }
}
