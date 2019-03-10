using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(long id);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(long id, Product product);
        Task DeleteProduct(long id);
    }
}