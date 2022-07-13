using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTraineesTask1.Models.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [NotMapped]
        [CompareAttribute("Password", ErrorMessage = "Password doesnt match")]
        public string? ConfrimPassword { get; set; }

        public Enum.Roles Role { get; set; } = Enum.Roles.RegularUser;
       
    }
}