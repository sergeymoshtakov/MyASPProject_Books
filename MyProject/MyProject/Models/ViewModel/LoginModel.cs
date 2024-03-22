using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Not set Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Not set password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
