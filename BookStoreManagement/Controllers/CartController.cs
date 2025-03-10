using Microsoft.AspNetCore.Mvc;
using DigitalBookStoreManagement.Models;
using DigitalBookStoreManagement.Repositories;

namespace DigitalBookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _cartRepository.GetCartByID(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            await _cartRepository.CreateCart(cart);
            return CreatedAtAction(nameof(GetCart), new { id = cart.CartID }, cart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, Cart cart)
        {
            if (id != cart.CartID)
            {
                return BadRequest();
            }

            await _cartRepository.UpdateCart(cart);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _cartRepository.GetCartByID(id);
            if (cart == null)
            {
                return NotFound();
            }

            await _cartRepository.DeleteCart(id);
            return NoContent();
        }


        [HttpPost("checkout/{cartId}")]
        public bool Checkout(int cartId)
        {
            var OrderCreated = _cartRepository.CheckOutCart(cartId);
            if (OrderCreated)
            {
                return true;
            }
            return false;
        }
        //[HttpPost("add-item-to-cart/{ cartId}")]
        //public IActionResult AddItemToCart(int cartId, [FromBody] CartItem cItem)
        //{
        //    var CheckIfAdded = _cartRepository.AddItemsToCart(cartId, cItem);
        //    if (CheckIfAdded)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

    }
}
