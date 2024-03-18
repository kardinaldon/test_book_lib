using System.ComponentModel.DataAnnotations;

namespace test_library.Models
{
    public class Book
    {

        [Key]
        public int BookID { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
    }
}
