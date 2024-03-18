using Dapper;
using Npgsql;
using test_library.Models;

namespace test_library.Respository
{
    public class DbContextData
    {
        private readonly IConfiguration _configuration;

        public DbContextData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Init()
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var sql = @"
                CREATE TABLE IF NOT EXISTS Book (
                    BookID SERIAL PRIMARY KEY,
                    Title VARCHAR,
                    Author VARCHAR
                );
            ";
            connection.Execute(sql);
            /*Book book = new Book();
            book.Author = "Test Author";
            book.Title = "Test Title";
            _respository.InsertBook(book);*/

        }
    }
}
