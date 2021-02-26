using System.ComponentModel.DataAnnotations;

namespace simple_messenger_backend.Models
{
    public class GetChatModel
    {
       [Required]
       public string User1 { get; set; }
       
       [Required]
       public string User2 { get ; set; }
    }
}