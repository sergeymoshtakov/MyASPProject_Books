using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? Year { get; set; }
    }
}
