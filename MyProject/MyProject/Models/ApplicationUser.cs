using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string? City { get; set; } // Місто
        public string? ProfilePhotoPath { get; set; }// Шлях до фото профілю
        public int? Year { get; set; } // Рік народження 
    }
}
