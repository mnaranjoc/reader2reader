using System.Data.Entity;

namespace reader2reader.Models
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}