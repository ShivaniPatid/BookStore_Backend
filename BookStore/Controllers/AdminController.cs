using BusinessLayer.Inerface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string emailId, string password)
        {
            try
            {
                var result = adminBL.AdminLogin(emailId, password);
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
    }
}
