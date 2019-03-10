using ElectronicsStoreApi.DomainModels;
using ElectronicsStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orders;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orders = orderRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllOrders()
        {
            List<Order> orders = await _orders.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(long id)
        {
            Order order = await _orders.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            Order createdOrder = await _orders.CreateOrder(order);

            return CreatedAtAction(
                nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(long id, [FromBody] Order order)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orders.UpdateOrder(id, order);
                return Ok();
            }
            catch (EntityNotFoundException<Order>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            await _orders.DeleteOrder(id);
            return NoContent();
        }

        [HttpGet("{id}/rows")]
        public async Task<IActionResult> GetRows(long id)
        {
            List<OrderRow> rows = await _orders.GetRowsForOrder(id);
            if (rows == null)
            {
                return NotFound();
            }

            return Ok(rows);
        }

        [HttpPost("{id}/rows")]
        public async Task<IActionResult> AddRow(long id, [FromBody] OrderRow row)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                OrderRow createdRow = await _orders.AddRowToOrder(id, row);

                return CreatedAtAction(
                    nameof(GetRow), new { orderId = id, rowId = createdRow.Id }, createdRow);
            }
            catch (EntityNotFoundException<Order>)
            {
                return NotFound();
            }
        }

        [HttpGet("{orderId}/rows/{rowId}")]
        public async Task<IActionResult> GetRow(long orderId, long rowId)
        {
            OrderRow row = await _orders.GetRowInOrder(orderId, rowId);
            if (row == null)
            {
                return NotFound();
            }

            return Ok(row);
        }

        [HttpPut("{orderId}/rows/{rowId}")]
        public async Task<IActionResult> UpdateRow(long orderId, long rowId, [FromBody] OrderRow row)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _orders.UpdateRowInOrder(orderId, rowId, row);
                return NoContent();
            }
            catch (EntityNotFoundException<OrderRow>)
            {
                return NotFound();
            }
            catch (EntityNotFoundException<Order>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{orderId}/rows/{rowId}")]
        public async Task<IActionResult> DeleteRow(long orderId, long rowId)
        {
            try
            {
                await _orders.DeleteRowFromOrder(orderId, rowId);
                return NoContent();
            }
            catch (EntityNotFoundException<Order>)
            {
                //If the order is not found, return 404
                return NotFound();
            }
        }
    }
}
