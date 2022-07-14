using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTraineesTask1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [NotMapped]
        [CompareAttribute("Password", ErrorMessage = "Password doesnt match")]
        public string? ConfrimPassword { get; set; }

        public string Role { get; set; } = "RegularUser";
       
    
    }
    
    // public class Verify{
    //     public bool isEmpty (string name) => (name == null);
    // }
}