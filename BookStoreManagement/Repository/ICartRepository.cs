using DigitalBookStoreManagement.Models;

namespace DigitalBookStoreManagement.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByID(int id);
        Task CreateCart(Cart cart);
        //bool AddItemsToCart(int cartId, CartItem newItem);
        Task UpdateCart(Cart cart);
        Task DeleteCart(int id);

        bool CheckOutCart(int cartId);
    }
}
