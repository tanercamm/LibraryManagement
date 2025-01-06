using LibraryManagement.Data.Repositories.Abstract;
using LibraryManagement.Entities;

namespace LibraryManagement.Data.Repositories.Concrete
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
    }
}
