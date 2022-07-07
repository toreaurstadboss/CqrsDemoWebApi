using CqrsDemoWebApi.Database;
using CqrsDemoWebApi.Features.Books;
using CqrsDemoWebApi.Features.Books.Command;
using CqrsDemoWebApi.Features.Books.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsDemoWebApi.Controllers
{

    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks() 
        {
            var books = await _mediator.Send(new GetBooks.Query());
            return books; 
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id)
        {
            var book = await _mediator.Send(new GetBooksById.Query { Id = id });
            return book; 
        }

        [HttpPost]
        public async Task<ActionResult> AddBook([FromBody] AddNewBook.Command command)
        {
            var createdBookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBook), new { id = createdBookId }, null); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _mediator.Send(new DeleteCommand.Command { Id = id });
            return NoContent(); 
        }

    }
}
