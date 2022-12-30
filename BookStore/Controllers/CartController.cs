using System;
using System.Linq;
using BusinessLayer.Inerface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize]
        [HttpPost("AddCart")]
        public IActionResult AddCart(int bookId, int quantity)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = cartBL.AddCart(userId, bookId, quantity);
                if (result)
                {
                    return Ok(new { success = true, message = "Book added to cart" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book not added !!" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("UpdateCart")]
        public IActionResult UpdateCart(int cartId, int quantity)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = cartBL.UpdateCart(quantity, userId, cartId);
                if (result)
                {
                    return Ok(new { success = true, message = "Cart Updated..." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cart not updated..." });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("DeleteCart")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = cartBL.DeleteCart(userId, cartId);
                if (result)
                {
                    return Ok(new { success = true, message = "Cart Deleted..." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetCart")]
        public IActionResult GetCartDetails()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = cartBL.GetCartDetails(userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Cart Details", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
