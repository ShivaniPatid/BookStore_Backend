using System.Linq;
using System;
using BusinessLayer.Inerface;
using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Service;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize]
        [HttpPost("AddFeddback")]
        public IActionResult AddFeddback(int bookId, FeedbackModel feedbackModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = feedbackBL.AddFeddback(userId, bookId, feedbackModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Feedback added", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Feedback not added !!", data = result });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetFeedback")]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = feedbackBL.GetFeedback(bookId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "All feedbacks", data = result });
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
        [HttpDelete("DeleteFeedback")]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

                var result = feedbackBL.DeleteFeedback(feedbackId, userId);
                if (result)
                {
                    return Ok(new { success = true, message = "Feedback Deleted..." });
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
