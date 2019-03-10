using ElectronicsStoreApi.ApiModels;
using ElectronicsStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orders;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orders = orderRepository;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            List<OrderModel> orders = await _orders.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrder(long id)
        {
            OrderModel order = await _orders.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel order)
        {
            OrderModel createdOrder = await _orders.CreateOrder(order);

            return CreatedAtAction(
                nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOrder(long id, [FromBody] OrderModel order)
        {
            try
            {
                await _orders.UpdateOrder(id, order);
                return Ok();
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            await _orders.DeleteOrder(id);
            return NoContent();
        }

        [HttpGet("{id}/rows")]
        [ProducesResponseType(typeof(List<OrderRowModel>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRows(long id)
        {
            List<OrderRowModel> rows = await _orders.GetRowsForOrder(id);
            if (rows == null)
            {
                return NotFound();
            }

            return Ok(rows);
        }

        [HttpPost("{id}/rows")]
        [ProducesResponseType(typeof(OrderRowModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddRow(long id, [FromBody] OrderRowModel row)
        {
            try
            {
                OrderRowModel createdRow = await _orders.AddRowToOrder(id, row);

                return CreatedAtAction(
                    nameof(GetRow), new { orderId = id, rowId = createdRow.Id }, createdRow);
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                return NotFound();
            }
        }

        [HttpGet("{orderId}/rows/{rowId}")]
        [ProducesResponseType(typeof(OrderRowModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRow(long orderId, long rowId)
        {
            OrderRowModel row = await _orders.GetRowInOrder(orderId, rowId);
            if (row == null)
            {
                return NotFound();
            }

            return Ok(row);
        }

        [HttpPut("{orderId}/rows/{rowId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRow(long orderId, long rowId, [FromBody] OrderRowModel row)
        {
            try
            {
                await _orders.UpdateRowInOrder(orderId, rowId, row);
                return NoContent();
            }
            catch (EntityNotFoundException<OrderRowModel>)
            {
                return NotFound();
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{orderId}/rows/{rowId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRow(long orderId, long rowId)
        {
            try
            {
                await _orders.DeleteRowFromOrder(orderId, rowId);
                return NoContent();
            }
            catch (EntityNotFoundException<OrderModel>)
            {
                //If the order is not found, return 404
                return NotFound();
            }
        }
    }
}
