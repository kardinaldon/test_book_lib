using Dapper;
using Npgsql;
using test_library.Models;

namespace test_library.Respository
{
    public class BookRespository : IBookRespository
    {

        private NpgsqlConnection connection;
        private readonly IConfiguration _configuration;

        public BookRespository(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));          
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            int rowsAffected = await this.connection.ExecuteAsync(@"DELETE FROM Book WHERE BookID = @BookID"
                , new { BookID = bookId });

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public Book GetBook(int bookId)
        {
            return connection.Query<Book>("SELECT BookId,Title,Author FROM Book WHERE BookID =@BookID"
                , new { BookID = bookId }).SingleOrDefault();
        }

        public List<Book> GetBooks()
        {
            return this.connection.Query<Book>("SELECT BookID,Title,Author FROM Book ORDER BY BookID").ToList();
        }

        public async Task<bool> InsertBook(Book book)
        {
            int rowsAffected = await this.connection.ExecuteAsync(@"INSERT INTO Book(Title,Author) values (@BookTitle, @BookAuthor)"
                , new { BookTitle = book.Title, BookAuthor = book.Author });

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateBook(Book book)
        {
            int rowsAffected = await this.connection.ExecuteAsync("UPDATE Book SET Title = @BookTitle, Author = @BookAuthor WHERE BookID = @BookID"
                , new { BookID = book.BookID, BookTitle = book.Title, BookAuthor = book.Author });

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
