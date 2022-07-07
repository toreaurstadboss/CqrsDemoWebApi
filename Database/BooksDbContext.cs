using CqrsDemoWebApi.Database.Seed;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemoWebApi.Database
{
    public class BooksDbContext : DbContext
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BooksDbContext(DbContextOptions<BooksDbContext> options,
            IWebHostEnvironment hostingEnvironment) : base(options) 
        {
           
            _hostingEnvironment = hostingEnvironment;
        }

        public DbSet<Book> Books { get; set; } = null!;  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            modelBuilder.SeedBooks(contentRootPath); 
        }
    }
}
