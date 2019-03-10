using ElectronicsStoreApi.DAL;
using ElectronicsStoreApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicsStoreApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDataContext _db;

        public OrderRepository(StoreDataContext db)
        {
            _db = db;
        }

        public OrderRow AddRowToOrder(long id, OrderRow row)
        {
            if (OrderExists(id) == false)
            {
                throw new EntityNotFoundException<Order>(id);
            }

            row.OrderId = id;
            _db.OrderRows.Add(row);
            _db.SaveChanges();

            return row;
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
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }
        }

        public void DeleteRowFromOrder(long orderId, long rowId)
        {
            if (OrderExists(orderId) == false)
            {
                throw new EntityNotFoundException<Order>(orderId);
            }

            OrderRow row = GetRowInOrder(orderId, rowId);
            if (row != null)
            {
                _db.OrderRows.Remove(row);
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

        public OrderRow GetRowInOrder(long orderId, long rowId)
        {
            return _db.OrderRows.FirstOrDefault(r => r.OrderId == orderId && r.Id == rowId);
        }

        public List<OrderRow> GetRowsForOrder(long id)
        {
            //Detect non-existing order
            if (OrderExists(id) == false)
            {
                return null;
            }

            return _db.OrderRows.AsNoTracking()
                        .Where(r => r.OrderId == id)
                        .ToList();
        }

        public void UpdateOrder(long id, Order updatedOrder)
        {
            Order order = GetOrder(id);
            if (order == null)
            {
                throw new EntityNotFoundException<Order>(id);
            }

            order.CustomerName = updatedOrder.CustomerName;
            order.CustomerAddress = updatedOrder.CustomerAddress;
            order.CustomerEmail = updatedOrder.CustomerEmail;

            _db.SaveChanges();
        }

        public void UpdateRowInOrder(long orderId, long rowId, OrderRow updatedRow)
        {
            if (OrderExists(orderId) == false)
            {
                throw new EntityNotFoundException<Order>(orderId);
            }

            OrderRow row = GetRowInOrder(orderId, rowId);
            if (row == null)
            {
                throw new EntityNotFoundException<OrderRow>(rowId);
            }

            row.ProductId = updatedRow.ProductId;
            row.Quantity = updatedRow.Quantity;
            row.SingleProductPrice = updatedRow.SingleProductPrice;

            _db.SaveChanges();
        }

        private bool OrderExists(long orderId)
        {
            return _db.Orders.Any(o => o.Id == orderId);
        }
    }
}
