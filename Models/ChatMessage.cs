using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_messenger_backend.Models
{
    [Table("Message")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "From is required")]
        public string From { get; set; }

        [Required(ErrorMessage = "From is required")]
        public string To { get; set; }

        [Required(ErrorMessage = "From is required")]
        public string MessageText { get; set; }

        public ChatChannel Channel { get; set;}
    }
}
