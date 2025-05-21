using Iress_API_bookshelf.EF.Models;
using Iress_API_bookshelf.Services.Abstractions;

namespace Iress_API_bookshelf.Services
{
    public class BusinessLogicService : IBusinessLogicService
    {
        private readonly IBookRepositoryService _BookRepositoryService;

        public BusinessLogicService(IBookRepositoryService BookRepositoryService)
        {
            _BookRepositoryService = BookRepositoryService;
        }
        public async Task<int> AddBook(Book book)
        {
            var savedFlag = await _BookRepositoryService.AddBook(book);
            return savedFlag;
        }

        public async Task<IList<Book>> GetAllBooks()
        {
            var allBooks = await _BookRepositoryService.GetAllBooks();

            return allBooks;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _BookRepositoryService.GetBookById(id);
            return book;
        }

        public async Task<int> UpdateBook(Book book)
        {
            var savedFlag = await _BookRepositoryService.UpdateBook(book);
            return savedFlag;
        }

        public async Task<int> DeleteBook(int id)
        {
            return await _BookRepositoryService.DeleteBook(id);
        }
    }
}
