using CqrsDemoWebApi.Database;
using MediatR;

namespace CqrsDemoWebApi.Features.Books.Command
{
    public class DeleteCommand
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly BooksDbContext _booksDbContext;

            public CommandHandler(BooksDbContext booksDbContext)
            {
                _booksDbContext = booksDbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _booksDbContext.Books.FindAsync(new object[] { request.Id }, cancellationToken);
                if (book == null)
                    return Unit.Value;
                _booksDbContext.Books.Remove(book); 
                await _booksDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;                 
            }
        }

    }
}
