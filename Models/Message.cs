using System;
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
        public User From { get; set; }

        [Required(ErrorMessage = "To is required")]
        public User To { get; set; }

        [Required(ErrorMessage = "MessageText is required")]
        public string MessageText { get; set; }

        [Required(ErrorMessage = "Send time is required")]
        public DateTime SendTime { get; set; }
    }
}
