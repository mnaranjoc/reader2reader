using reader2reader.Models;
using PagedList;

namespace reader2reader.ViewModels
{
    public class BooksViewModel
    {
        public IPagedList<Book> Books { get; set; }
        public string Search { get; set; }
    }
}