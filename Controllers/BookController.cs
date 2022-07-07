using CqrsDemoWebApi.Database;
using CqrsDemoWebApi.Features.Books;
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

        [HttpGet("GetBooks")]
        public async Task<IEnumerable<Book>> GetBooks() 
        {
            var books = await _mediator.Send(new GetBooks.Query());
            return books; 
        }

    }
}
