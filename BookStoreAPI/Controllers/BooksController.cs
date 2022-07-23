using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        //tryimg to get our data from repository using DI
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet("")]
        //creating an action method that will handle the incoming http request

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();   
            return Ok(books);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksById([FromRoute] int id)
        {
            var book = await _bookRepository.GetBooksByIdAsync(id);

            if (book==null)
            {
                return NotFound();

            }
            return Ok(book);

        }

        [HttpPost("")]
        public async Task<IActionResult> AddBooks([FromBody] BookModel bookModel)
        {
            var id = await _bookRepository.AddBooksAsync(bookModel);
            //the method is going to return the id of the new book

            
            return CreatedAtAction(nameof(GetBooksById), new {id = id, Controller= "books"}, id);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooks([FromBody]BookModel bookModel, [FromRoute]int id)
        {
            await _bookRepository.UpdateBooksAsync(id, bookModel);
            return Ok();
           
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] int id)
        {
            await _bookRepository.UpdateBookPatchAsync(id, bookModel);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooks([FromRoute] int id)
        {
            await _bookRepository.DeleteBooksAsync(id);
            return Ok();

        }

    }
}
