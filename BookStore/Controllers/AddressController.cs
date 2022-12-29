using System.Linq;
using System;
using BusinessLayer.Inerface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonLayer.Models;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

            var result = addressBL.AddAddress(userId, addressModel);
            if (result)
            {
                return Ok(new { success = true, message = "Address added" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Address not added !!" });

            }
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateAddress(AddressModel addressModel)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

            var result = addressBL.UpdateAddress(userId, addressModel);
            if (result)
            {
                return Ok(new { success = true, message = "Address Updated..." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Address not updated..." });

            }
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = addressBL.GetAllAddress(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "All addresses", data = result });
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
        public IActionResult DeleteAddress(int addressId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

            var result = addressBL.DeleteAddress(userId, addressId);
            if (result)
            {
                return Ok(new { success = true, message = "Address Deleted..." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something went wrong..." });

            }
        }
    }
}
