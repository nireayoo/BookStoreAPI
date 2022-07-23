using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBooksByIdAsync(int bookId);
        Task<int> AddBooksAsync(BookModel bookmodel);
        Task UpdateBooksAsync(int bookId, BookModel bookmodel);
        Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel);
        Task DeleteBooksAsync(int bookId);


    }
}