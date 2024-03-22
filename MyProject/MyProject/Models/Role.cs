using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public Role()
        {
            Users = new List<ApplicationUser>();
        }
    }
}
