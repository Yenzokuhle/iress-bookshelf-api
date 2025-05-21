using Iress_API_bookshelf.EF.Context;
using Iress_API_bookshelf.EF.Models;
using Iress_API_bookshelf.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Iress_API_bookshelf.Services
{
    public class BookRepositoryService: IBookRepositoryService

    {
        private readonly BooksContext _BookContext;

        public BookRepositoryService(BooksContext BookContext)
        {
            _BookContext = BookContext;
        }

        public async Task<int> AddBook(Book book)
        {
            await _BookContext.Books.AddAsync(book);
            var savedflag = await _BookContext.SaveChangesAsync();

            return savedflag;
        }

        public async Task<IList<Book>> GetAllBooks()
        {
            var allBook = await _BookContext.Books.ToListAsync();

            return allBook;
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _BookContext.Books.Where(book => book.Id == id).FirstOrDefaultAsync();

            return book;
        }

        public async Task<int> UpdateBook(Book book)
        {
            _BookContext.Entry(book).State = EntityState.Modified;
            //_BookContext.Books.Update(book);
            var savedFlag = await _BookContext.SaveChangesAsync();
            return savedFlag;
        }

        public async Task<int> DeleteBook(int id)
        {
            var bookToDelete = await _BookContext.Books.SingleAsync(book => book.Id == id);

            if (bookToDelete == null)
            {
                return 0;
            }

            _BookContext.Remove(bookToDelete);
            return await _BookContext.SaveChangesAsync();

        }
    }
}
