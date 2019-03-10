using ElectronicsStoreApi.ApiModels;
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

        public async Task<OrderRowModel> AddRowToOrder(long id, OrderRowModel model)
        {
            if (await OrderExists(id) == false)
            {
                throw new EntityNotFoundException<OrderModel>(id);
            }

            var row = new OrderRow();
            model.MapTo(row);

            row.OrderId = id;
            await _db.OrderRows.AddAsync(row);
            await _db.SaveChangesAsync();

            return row.MapToDto();
        }

        public async Task<OrderModel> CreateOrder(OrderModel model)
        {
            var order = new Order();
            model.MapTo(order);

            order.CreatedAt = DateTimeOffset.Now;

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return order.MapToDto();
        }

        public async Task DeleteOrder(long id)
        {
            Order order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
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
                throw new EntityNotFoundException<OrderModel>(orderId);
            }

            OrderRow row = await _db.OrderRows.FirstOrDefaultAsync(r => r.OrderId == orderId && r.Id == rowId);
            if (row != null)
            {
                _db.OrderRows.Remove(row);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            return (await _db.Orders.AsNoTracking().ToListAsync())
                .Select(o => o.MapToDto())
                .ToList();
        }

        public async Task<OrderModel> GetOrder(long id)
        {
            return (await _db.Orders.FirstOrDefaultAsync(o => o.Id == id))?.MapToDto();
        }

        public async Task<OrderRowModel> GetRowInOrder(long orderId, long rowId)
        {
            return (await _db.OrderRows.FirstOrDefaultAsync(r => r.OrderId == orderId && r.Id == rowId))?.MapToDto();
        }

        public async Task<List<OrderRowModel>> GetRowsForOrder(long id)
        {
            // Detect non-existing order
            if (await OrderExists(id) == false)
            {
                return null;
            }

            return (await _db.OrderRows.AsNoTracking()
                .Where(r => r.OrderId == id)
                .ToListAsync())
                    .Select(r => r.MapToDto())
                    .ToList();
        }

        public async Task UpdateOrder(long id, OrderModel updatedOrder)
        {
            Order order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new EntityNotFoundException<OrderModel>(id);
            }

            updatedOrder.MapTo(order);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRowInOrder(long orderId, long rowId, OrderRowModel updatedRow)
        {
            if (await OrderExists(orderId) == false)
            {
                throw new EntityNotFoundException<OrderModel>(orderId);
            }

            OrderRow row = await _db.OrderRows.FirstOrDefaultAsync(r => r.OrderId == orderId && r.Id == rowId);
            if (row == null)
            {
                throw new EntityNotFoundException<OrderRowModel>(rowId);
            }

            updatedRow.MapTo(row);
            await _db.SaveChangesAsync();
        }

        private async Task<bool> OrderExists(long orderId)
        {
            return await _db.Orders.AnyAsync(o => o.Id == orderId);
        }
    }
}
