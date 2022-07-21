using BookStoreAPI.Models;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBooksByIdAsync(int bookId);
        Task<int> AddBooksAsync(BookModel bookmodel);
        Task UpdateBooksAsync(int bookId, BookModel bookmodel);
    }
}