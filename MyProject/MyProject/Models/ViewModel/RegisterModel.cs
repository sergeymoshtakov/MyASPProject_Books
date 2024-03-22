using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.ViewModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Not set Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Not set Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Not set password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not correct")]
        public string ConfirmPassword { get; set; }

        public string? City { get; set; } // Місто
        public string? ProfilePhotoPath { get; set; }// Шлях до фото профілю
        public int? Year { get; set; } // Рік народження 
    }
}
