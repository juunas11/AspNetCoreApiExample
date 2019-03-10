using ElectronicsStoreApi.DAL;
using ElectronicsStoreApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<Product> CreateProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(long id)
        {
            Product product = await GetProduct(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _db.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetProduct(long id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProduct(long id, Product updatedProduct)
        {
            Product product = await GetProduct(id);

            if (product == null)
            {
                throw new EntityNotFoundException<Product>(id);
            }

            product.Name = updatedProduct.Name;
            product.Category = updatedProduct.Category;
            product.Price = updatedProduct.Price;

            await _db.SaveChangesAsync();
        }
    }
}
