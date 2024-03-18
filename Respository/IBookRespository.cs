using test_library.Models;

namespace test_library.Respository
{
    public interface IBookRespository
    {
        List<Book> GetBooks();
        Book GetBook(int bookId);
        Task<bool> InsertBook(Book book);
        Task<bool> DeleteBook(int bookId);
        Task<bool> UpdateBook(Book book);
    }
}
