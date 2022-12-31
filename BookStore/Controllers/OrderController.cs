using System.Linq;
using System;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Service;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [Authorize]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderModel ordersModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = orderBL.AddOrder(ordersModel, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Placed Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went wrong..." });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = orderBL.GetAllOrder(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Fetched Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Something went wrong..." });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = orderBL.DeleteOrder(orderId, userId);
                if (result)
                {
                    return Ok(new { success = true, message = "Order Deleted..." });
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
