using System.ComponentModel.DataAnnotations;

namespace simple_messenger_backend.Models
{
    public class SignUpModel
    {
       [Required]
       public string Email { get; set; }
       [Required]
       public string Username { get; set; }

       [Required]
       public string Password { get ; set; }
    }
}