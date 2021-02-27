using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_messenger_backend.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {get; set;}

        [Required(ErrorMessage = "Email is required")]
        public string Email {get; set;}
        
        [Required(ErrorMessage = "Username is required")]
        public string Username {get; set;}

        [Required(ErrorMessage = "Password is required")]
        public string Password {get; set;}

        [Required(ErrorMessage = "Registration Date needed")]
        public string RegistrationDate {get; set;} 

        [NotMapped]
        public string AuthenticationData { get; set; }

        [Required(ErrorMessage = "Connection Status is required")]
        public bool Connected {get; set;}
    }
}