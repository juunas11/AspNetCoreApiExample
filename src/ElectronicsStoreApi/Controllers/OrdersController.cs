using ElectronicsStoreApi.DomainModels;
using ElectronicsStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicsStoreApi.Hateoas;

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
        public IActionResult GetAllOrders()
        {
            List<Order> orders = _orders.GetAllOrders();
            var wrapper=  new LinkWrapper(orders);
            wrapper.Links.Add(new LinkValue("create", Url.Action(nameof(CreateOrder), "Orders", null, Request.Scheme)));
            return Ok(wrapper);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(long id)
        {
            Order order = _orders.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            var wrapper = new LinkWrapper(order);
            wrapper.Links.Add(new LinkValue("self", Url.Action(nameof(GetOrder), "Orders", new {id}, Request.Scheme)));
            wrapper.Links.Add(new LinkValue("rows", Url.Action(nameof(GetRows), "Orders", new {id}, Request.Scheme)));
            wrapper.Links.Add(new LinkValue("edit", Url.Action(nameof(UpdateOrder), "Orders", new {id}, Request.Scheme)));
            wrapper.Links.Add(new LinkValue("delete", Url.Action(nameof(DeleteOrder), "Orders", new {id}, Request.Scheme)));

            return Ok(wrapper);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            Order createdOrder = _orders.CreateOrder(order);

            return CreatedAtAction(
                nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(long id, [FromBody] Order order)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _orders.UpdateOrder(id, order);
                return Ok();
            }
            catch (EntityNotFoundException<Order>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(long id)
        {
            _orders.DeleteOrder(id);
            return Ok();
        }

        [HttpGet("{id}/rows")]
        public IActionResult GetRows(long id)
        {
            List<OrderRow> rows = _orders.GetRowsForOrder(id);
            if(rows == null)
            {
                return NotFound();
            }
            return Ok(rows);
        }

        [HttpPost("{id}/rows")]
        public IActionResult AddRow(long id, [FromBody] OrderRow row)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                OrderRow createdRow = _orders.AddRowToOrder(id, row);

                return CreatedAtAction(
                    nameof(GetRow), new { orderId = id, rowId = createdRow.Id }, createdRow);
            }
            catch (EntityNotFoundException<Order>)
            {
                return NotFound();
            }
        }

        [HttpGet("{orderId}/rows/{rowId}")]
        public IActionResult GetRow(long orderId, long rowId)
        {
            OrderRow row = _orders.GetRowInOrder(orderId, rowId);
            if(row == null)
            {
                return NotFound();
            }
            return Ok(row);
        }

        [HttpPut("{orderId}/rows/{rowId}")]
        public IActionResult UpdateRow(long orderId, long rowId, [FromBody] OrderRow row)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _orders.UpdateRowInOrder(orderId, rowId, row);
                return Ok();
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
        public IActionResult DeleteRow(long orderId, long rowId)
        {
            try
            {
                _orders.DeleteRowFromOrder(orderId, rowId);
                return Ok();
            }
            catch (EntityNotFoundException<Order>)
            {
                //If the order is not found, return 404
                return NotFound();
            }
        }
    }
}
