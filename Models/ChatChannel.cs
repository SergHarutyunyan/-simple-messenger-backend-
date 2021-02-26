using simple_messenger_backend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_messenger_backend.Models
{   
    [Table("Channel")]
    public class ChatChannel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "User1 is required")]
        public string User1 {get; set;}

        [Required(ErrorMessage = "User2 is required")]
        public string User2 {get; set;}

        public ICollection<Message> Messages {get;set;}
    }
}