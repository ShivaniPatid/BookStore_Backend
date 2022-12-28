using BusinessLayer.Inerface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = bookBL.AddBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book Added Unsuccessfully" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(BookModel bookModel)
        {
            try
            {
                var result = bookBL.UpdateBook(bookModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Book Updated !!", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book not Updated !?" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = bookBL.DeleteBook(bookId);

                if (result)
                {
                    return Ok(new { success = true, message = "Book Deleted Successfully..." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = bookBL.GetAllBooks();

                if (result != null)
                {
                    return Ok(new { success = true, message = "All Books !!", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetBook")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var result = bookBL.GetBookByBookId(bookId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Book Details Fetched Sucessfully...", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
