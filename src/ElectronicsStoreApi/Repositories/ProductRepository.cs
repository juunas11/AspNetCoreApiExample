using ElectronicsStoreApi.DAL;
using ElectronicsStoreApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDataContext _db;

        public ProductRepository(StoreDataContext db)
        {
            _db = db;
        }

        public Product CreateProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public void DeleteProduct(long id)
        {
            Product product = GetProduct(id);
            if(product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
        }

        public List<Product> GetAllProducts()
        {
            return _db.Products.AsNoTracking().ToList();
        }

        public Product GetProduct(long id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(long id, Product updatedProduct)
        {
            Product product = GetProduct(id);

            if(product == null)
            {
                throw new EntityNotFoundException<Product>(id);
            }

            product.Name = updatedProduct.Name;
            product.Category = updatedProduct.Category;
            product.Price = updatedProduct.Price;

            _db.SaveChanges();
        }
    }
}
