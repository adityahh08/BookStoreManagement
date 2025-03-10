using DigitalBookStoreManagement.Data;
using DigitalBookStoreManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContextClass _context;

        public CartRepository(ApplicationDbContextClass context)
        {
            _context = context;
        }
      


        public async Task<Cart> GetCartByID(int id)
        {
            return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.CartID == id);
        }

        public async Task CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        //public bool AddItemsToCart(int cartId, CartItem newItem)
        //{
        //    var cart = _context.Carts.Include(ci=> ci.CartItems).FirstOrDefault(c => c.CartID == cartId);
        //    if (cart != null)
        //    {
        //        cart.CartItems.Select(ci => new CartItem
        //        {
        //            CartID = newItem.CartID,
        //            BookID = newItem.BookID,
        //            Price = newItem.Price,
        //            Quantity = newItem.Quantity,
        //            TotalAmount = newItem.TotalAmount




        //        }).ToList();


        //    }
        //    return _context.SaveChanges() > 0;


        //}

        public async Task UpdateCart(Cart cart)
        {
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public bool CheckOutCart(int cartId)
        {
            var cart = _context.Carts.Include(c=>c.CartItems).FirstOrDefault(c => c.CartID == cartId);
            if(cart == null)
            {
                return false;
            }
            Order NewOrder = new Order
            {
                UserID = cart.UserID,
                OrderStatus = "Pending",
                OrderDate = DateTime.Now,
                PaymentStatus = "Not Paid",
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    BookID = ci.BookID,
                    Price = ci.Price,
                    Quantity = ci.Quantity,
                    TotalAmount = ci.Price*ci.Quantity
                }).ToList()


            };
            _context.Orders.Add(NewOrder);
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
