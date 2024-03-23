using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? City { get; set; } // Місто
        public string? ProfilePhotoPath { get; set; }// Шлях до фото профілю
        public int? Year { get; set; } // Рік народження 
    }
}
