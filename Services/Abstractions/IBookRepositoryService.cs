using Iress_API_bookshelf.EF.Models;

namespace Iress_API_bookshelf.Services.Abstractions
{
    public interface IBookRepositoryService
    {
        public Task<int> AddBook(Book book);
        public Task<IList<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task<int> UpdateBook(Book book);
        public Task<int> DeleteBook(int id);
    }
}
