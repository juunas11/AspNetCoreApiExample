using System.Collections.Generic;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
    }
}