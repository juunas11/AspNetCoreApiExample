using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrder(long id);
        Task<Order> CreateOrder(Order order);
        Task UpdateOrder(long id, Order updatedOrder);
        Task DeleteOrder(long id);
        Task<List<OrderRow>> GetRowsForOrder(long id);
        Task<OrderRow> AddRowToOrder(long id, OrderRow row);
        Task<OrderRow> GetRowInOrder(long orderId, long rowId);
        Task UpdateRowInOrder(long orderId, long rowId, OrderRow updatedRow);
        Task DeleteRowFromOrder(long orderId, long rowId);
    }
}