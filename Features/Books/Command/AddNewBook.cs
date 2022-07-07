using CqrsDemoWebApi.Database;
using MediatR;

namespace CqrsDemoWebApi.Features.Books.Command
{
    public class AddNewBook
    {
        public class Command : IRequest<int>
        {
            public string? Title { get; set; }
            public string? Author { get; set; }
            public int Year { get; set; }
            public string? ImageLink { get; set; }
            public int Pages { get; set; }
            public string? Country { get; set; }
            public string? Language { get; set; }
            public string? Link { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly BooksDbContext _booksDbContext;

            public CommandHandler(BooksDbContext booksDbContext)
            {
                _booksDbContext = booksDbContext;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = new Book
                {
                    Title = request.Title,
                    Author = request.Author,    
                    Year = request.Year,
                    ImageLink = request.ImageLink,
                    Pages = request.Pages,
                    Country = request.Country,
                    Language = request.Language,
                    Link = request.Link
                };

                await _booksDbContext.Books.AddAsync(book, cancellationToken);
                _booksDbContext.SaveChanges();
                return book.BookId;                          
            }
        }

    }
}
