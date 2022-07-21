using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class BooksRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BooksRepository(BookStoreContext context)
        {

            _context = context;
        }
        //i want to return the list of books available in a database
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            /*var records = _context.Books.ToListAsync();*/ //we want to select our books directly from the book modrl
            var records = await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description

            }).ToListAsync();

            return records;
        }
        public async Task<BookModel> GetBooksByIdAsync(int bookId)
        {
            var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description

            }).FirstOrDefaultAsync();

            return records;
        }
        public async Task<int> AddBooksAsync(BookModel bookmodel)//object of the book
        {
            //trying to convert the data of bookmodel to the book
            var book = new Books()
            {
                Title = bookmodel.Title,
                Description = bookmodel.Description
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBooksAsync(int bookId, BookModel bookmodel)
        {

            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.Title = bookmodel.Title;
                book.Description = bookmodel.Description;
                
               await _context.SaveChangesAsync(); 

            }
            
        }
    }
}
