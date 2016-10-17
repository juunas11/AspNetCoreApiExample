using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicsStoreApi.DomainModels;
using ElectronicsStoreApi.DAL;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStoreApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDataContext _db;

        public OrderRepository(StoreDataContext db)
        {
            _db = db;
        }

        public Order CreateOrder(Order order)
        {
            order.CreatedAt = DateTimeOffset.Now;

            _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }

        public void DeleteOrder(long id)
        {
            Order order = GetOrder(id);
            if(order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }
        }

        public List<Order> GetAllOrders()
        {
            return _db.Orders.AsNoTracking().ToList();
        }

        public Order GetOrder(long id)
        {
            return _db.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(long id, Order updatedOrder)
        {
            Order order = GetOrder(id);
            if(order == null)
            {
                throw new EntityNotFoundException<Order>(id);
            }

            order.CustomerName = updatedOrder.CustomerName;
            order.CustomerAddress = updatedOrder.CustomerAddress;
            order.CustomerEmail = updatedOrder.CustomerEmail;

            _db.SaveChanges();
        }
    }
}
