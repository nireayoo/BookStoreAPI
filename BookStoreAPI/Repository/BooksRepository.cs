using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class BooksRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper mapper;

        public BooksRepository(BookStoreContext context, IMapper mapper)
        {

            _context = context;
            this.mapper = mapper;
        }
        //i want to return the list of books available in a database
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            /*var records = _context.Books.ToListAsync();*/ //we want to select our books directly from the book model
            var records = await _context.Books.ToListAsync();
            return mapper.Map<List<BookModel>>(records);
        }
        public async Task<BookModel> GetBooksByIdAsync(int bookId)
        {
            //var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description

            //}).FirstOrDefaultAsync();

            //return records;

            var book = await _context.Books.FindAsync(bookId);
            return mapper.Map<BookModel>(book); //we have to pass the source
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

        //public async Task UpdateBooksAsync(int bookId, BookModel bookmodel)
        //{

        //    var book = await _context.Books.FindAsync(bookId);
        //    if (book != null)
        //    {
        //        book.Title = bookmodel.Title;
        //        book.Description = bookmodel.Description;

        //       await _context.SaveChangesAsync(); 

        //    }

        //}

        public async Task UpdateBooksAsync(int bookId, BookModel bookmodel)
        {
            var book = new Books()
            {
                Id = bookId,
                Title = bookmodel.Title,
                Description = bookmodel.Description
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();

            }

        }

        public async Task DeleteBooksAsync(int bookId)//to delete a single book in the database
        {

            var book = new Books(){Id = bookId};
            _context.Books.Remove(book);
           await _context.SaveChangesAsync();
                                  
        }

    }
}