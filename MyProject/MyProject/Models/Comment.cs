using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
