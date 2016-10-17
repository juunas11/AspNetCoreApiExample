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

        public List<Product> GetAllProducts()
        {
            return _db.Products.AsNoTracking().ToList();
        }
    }
}
