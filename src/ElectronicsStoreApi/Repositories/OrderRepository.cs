using ElectronicsStoreApi.DAL;
using ElectronicsStoreApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDataContext _db;

        public OrderRepository(StoreDataContext db)
        {
            _db = db;
        }

        public async Task<OrderRow> AddRowToOrder(long id, OrderRow row)
        {
            if (await OrderExists(id) == false)
            {
                throw new EntityNotFoundException<Order>(id);
            }

            row.OrderId = id;
            await _db.OrderRows.AddAsync(row);
            await _db.SaveChangesAsync();

            return row;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            order.CreatedAt = DateTimeOffset.Now;

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task DeleteOrder(long id)
        {
            Order order = await GetOrder(id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteRowFromOrder(long orderId, long rowId)
        {
            if (await OrderExists(orderId) == false)
            {
                throw new EntityNotFoundException<Order>(orderId);
            }

            OrderRow row = await GetRowInOrder(orderId, rowId);
            if (row != null)
            {
                _db.OrderRows.Remove(row);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _db.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order> GetOrder(long id)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<OrderRow> GetRowInOrder(long orderId, long rowId)
        {
            return await _db.OrderRows.FirstOrDefaultAsync(r => r.OrderId == orderId && r.Id == rowId);
        }

        public async Task<List<OrderRow>> GetRowsForOrder(long id)
        {
            // Detect non-existing order
            if (await OrderExists(id) == false)
            {
                return null;
            }

            return await _db.OrderRows.AsNoTracking()
                .Where(r => r.OrderId == id)
                .ToListAsync();
        }

        public async Task UpdateOrder(long id, Order updatedOrder)
        {
            Order order = await GetOrder(id);
            if (order == null)
            {
                throw new EntityNotFoundException<Order>(id);
            }

            order.CustomerName = updatedOrder.CustomerName;
            order.CustomerAddress = updatedOrder.CustomerAddress;
            order.CustomerEmail = updatedOrder.CustomerEmail;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateRowInOrder(long orderId, long rowId, OrderRow updatedRow)
        {
            if (await OrderExists(orderId) == false)
            {
                throw new EntityNotFoundException<Order>(orderId);
            }

            OrderRow row = await GetRowInOrder(orderId, rowId);
            if (row == null)
            {
                throw new EntityNotFoundException<OrderRow>(rowId);
            }

            row.ProductId = updatedRow.ProductId;
            row.Quantity = updatedRow.Quantity;
            row.SingleProductPrice = updatedRow.SingleProductPrice;

            await _db.SaveChangesAsync();
        }

        private async Task<bool> OrderExists(long orderId)
        {
            return await _db.Orders.AnyAsync(o => o.Id == orderId);
        }
    }
}
