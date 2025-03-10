

using DigitalBookStoreManagement.Data;
using DigitalBookStoreManagement.Models;
using DigitalBookStoreManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DigitalBookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [HttpGet("get-orders")]
        public ActionResult<List<Order>> GetOrders()
        {
            var order = _orderRepository.GetAllOrder();
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrderByOrderId(int orderId)
        {
            var order = _orderRepository.GetOrderByOrderId(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Order>> GetOrderByUserId(int userId)
        {
            var order = _orderRepository.GetOrderByUserId(userId);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public ActionResult<bool> PlaceOrder(Order order)
        {
            var result = _orderRepository.PlaceOrder(order);
            if (result)
            {
                return CreatedAtAction(nameof(GetOrderByOrderId), new { orderId = order.OrderID }, order);
            }
            return BadRequest();
        }


        [HttpDelete("{orderId}")]
        public ActionResult<bool> CancelOrder(int orderId)
        {
            var result = _orderRepository.CancelOrder(orderId);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{orderId}/order-status")]
        public ActionResult<bool> UpdateStatus(int orderId, [FromBody] string status)
        {
            var result = _orderRepository.UpdateStatus(orderId, status);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }



        [HttpPut("{id}/update-total")]
        public IActionResult UpdateOrderTotal(int id)
        {
             _orderRepository.UpdateOrderTotal(id);
            var updatedOrder =  _orderRepository.GetOrderByOrderId(id);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return Ok(updatedOrder);
        }

    }
}

