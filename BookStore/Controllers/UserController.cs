using System.Security.Claims;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult AddUser(UserModel userModel)
        {
            try
            {
                var result = userBL.Register(userModel);
                if (result != null)
                    return Ok(new { success = true, message = "Registration succsessfull", data = result });
                else
                    return BadRequest(new { success = false, message = "Registration Unsuccessfull" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(string emailId, string password)
        {
            try
            {
                var result = userBL.Login(emailId,password);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login succsessfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Login Unsuccsessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = userBL.ForgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Email sent successfully", data=result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Email has not sent" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)

        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(email, password, confirmPassword);
                if (result != false)
                {
                    return this.Ok(new { success = true, message = "Your password has been reset" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Try again" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult GetUSer()
        {
            try
            {
                var emailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.GetUser(emailId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Get User Details", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "something went wrong" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
