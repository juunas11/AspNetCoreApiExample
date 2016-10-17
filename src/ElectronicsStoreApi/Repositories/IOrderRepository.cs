using System.Collections.Generic;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrder(long id);
        Order CreateOrder(Order order);
        void UpdateOrder(long id, Order updatedOrder);
        void DeleteOrder(long id);
        List<OrderRow> GetRowsForOrder(long id);
        OrderRow AddRowToOrder(long id, OrderRow row);
        OrderRow GetRowInOrder(long orderId, long rowId);
        void UpdateRowInOrder(long orderId, long rowId, OrderRow updatedRow);
        void DeleteRowFromOrder(long orderId, long rowId);
    }
}