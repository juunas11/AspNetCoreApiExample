using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicsStoreApi.ApiModels;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProduct(long id);
        Task<ProductModel> CreateProduct(ProductModel product);
        Task UpdateProduct(long id, ProductModel product);
        Task DeleteProduct(long id);
    }
}