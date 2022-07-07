using CqrsDemoWebApi.Database;
using MediatR;

namespace CqrsDemoWebApi.Features.Books.Query
{
  
    public class GetBooksById
    {

        public class Query : IRequest<Book>
        {
            public int Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Book>
        {
            private readonly BooksDbContext _booksDbContext;

            public QueryHandler(BooksDbContext booksDbContext)
            {
                _booksDbContext = booksDbContext;
            }

            public async Task<Book> Handle(Query request, CancellationToken cancellationToken)
            {
                var book = await _booksDbContext.Books.FindAsync(new object[] { request.Id }, cancellationToken);
                return book ?? new Book { BookId = -1, Title = "No title found with this i"}; 
            }
        }


    }

}
