using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicsStoreApi.ApiModels;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrder(long id);
        Task<OrderModel> CreateOrder(OrderModel order);
        Task UpdateOrder(long id, OrderModel updatedOrder);
        Task DeleteOrder(long id);
        Task<List<OrderRowModel>> GetRowsForOrder(long id);
        Task<OrderRowModel> AddRowToOrder(long id, OrderRowModel row);
        Task<OrderRowModel> GetRowInOrder(long orderId, long rowId);
        Task UpdateRowInOrder(long orderId, long rowId, OrderRowModel updatedRow);
        Task DeleteRowFromOrder(long orderId, long rowId);
    }
}