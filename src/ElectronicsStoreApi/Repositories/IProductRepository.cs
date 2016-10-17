using System.Collections.Generic;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProduct(long id);
        Product CreateProduct(Product product);
        void UpdateProduct(long id, Product product);
        void DeleteProduct(long id);
    }
}