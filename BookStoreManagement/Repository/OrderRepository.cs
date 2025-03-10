
using DigitalBookStoreManagement.Data;
using DigitalBookStoreManagement.Models;
using DigitalBookStoreManagement.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContextClass _context;

    public OrderRepository(ApplicationDbContextClass context)
    {
        _context = context;
    }

    public List<Order> GetAllOrder()
    {
        return _context.Orders.Include(o => o.OrderItems).ToList();
    }

    public Order GetOrderByOrderId(int orderId)
    {
        return _context.Orders.Include(o => o.OrderItems)
                              .FirstOrDefault(o => o.OrderID == orderId);
    }

    public List<Order> GetOrderByUserId(int userId)
    {
        return _context.Orders.Include(o => o.OrderItems)
                              .Where(o => o.UserID == userId).ToList();
    }

    public bool PlaceOrder(Order order)
    {
        _context.Orders.Add(order);


        return _context.SaveChanges() > 0;
    }

    public bool CancelOrder(int orderId)
    {

        var order = _context.Orders
       .Include(o => o.OrderItems)
       .FirstOrDefault(o => o.OrderID == orderId);

        if (order == null)
        {
            return false;
        }


        _context.Orders.Remove(order);

        _context.SaveChanges();

        return true;

    }

    public bool UpdateStatus(int orderId, string status)
    {
        var order = _context.Orders.Find(orderId);
        if (order != null)
        {
            order.OrderStatus = status;
            return _context.SaveChanges() > 0;
        }
        return false;
    }

 

    public bool UpdateOrderTotal(int orderId)
    {
        Order order = GetOrderByOrderId(orderId);
        if (order != null)
        {
            order.TotalAmount = order.OrderItems.Sum(item => item.TotalAmount);
            return _context.SaveChanges() > 0;
        }
        return false;


    }
}
































