using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CqrsDemoWebApi.Database.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void SeedBooks(this ModelBuilder modelBuilder, string contentRootPath)
        {
            string jsonInput = File.ReadAllText(Path.Combine(contentRootPath, @"Database/Seed/books.json")); 
            var books = JsonConvert.DeserializeObject<Book[]>(jsonInput);

            int bookId = 0;
            foreach (var book in books)
            {
                bookId++;
                book.BookId = bookId; 
            }
           
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<Book>().Property(b => b.BookId).ValueGeneratedOnAdd();                 
        }
    }
}
