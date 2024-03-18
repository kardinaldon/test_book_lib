using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test_library.Models;
using test_library.Respository;

namespace test_library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookRespository _respository;

        public BookController(IBookRespository respository)
        {
             _respository = respository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = _respository.GetBooks();
            return Ok(books);
        }

        // GET book/1
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = _respository.GetBook(id);
            if (book!=null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound(new { Message = "book Not Found" });
            }
            
        }

        // POST book
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {

            bool result = await _respository.InsertBook(book);
            if (result)
            {
                return Ok(new { Message = "Book Created" });
            }
            else
            {
                return NotFound(new { Message = "book Not Found" });
            }

        }

        // PUT book
        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put(Book book)
        {
            
            bool result = await _respository.UpdateBook(book);
            if (result)
            {
                return Ok(new { Message = "book Updated" });
            }
            else
            {
                return NotFound(new { Message = "book Not Found" });
            }

        }

        // DELETE book/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _respository.DeleteBook(id);
            if (result)
            {
                return Ok(new { Message = "book Deleted" });
            }
            else { 
                return NotFound(new { Message = "book Not Found" });
            }
        }
    }
}
