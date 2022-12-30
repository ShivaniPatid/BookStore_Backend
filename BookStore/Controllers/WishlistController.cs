using System.Linq;
using System;
using BusinessLayer.Inerface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;
        public WishlistController (IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddToWishlist(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = wishlistBL.AddToWishlist(userId, bookId);
                if (result)
                {
                    return Ok(new { success = true, message = "Book added to wishlist" });
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
        [HttpGet("View")]
        public IActionResult ViewWishlist()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = wishlistBL.ViewWishlist(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Wishlist Details", data = result });
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
        [HttpDelete("Delete")]
        public IActionResult DeleteFromWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = wishlistBL.DeleteFromWishlist(userId, wishlistId);
                if (result)
                {
                    return Ok(new { success = true, message = "Book removed from wishlist" });
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
    