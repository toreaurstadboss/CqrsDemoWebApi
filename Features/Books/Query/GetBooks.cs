using CqrsDemoWebApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemoWebApi.Features.Books.Query
{
    public class GetBooks
    {

        public class Query : IRequest<IEnumerable<Book>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Book>>
        {
            private readonly BooksDbContext _db;

            public QueryHandler(BooksDbContext db)
            {
                _db = db;
            }

            public async Task<IEnumerable<Book>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Books.ToListAsync(cancellationToken); 
            }
        }
    }
}
