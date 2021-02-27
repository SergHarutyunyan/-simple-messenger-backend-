using simple_messenger_backend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_messenger_backend.Models
{   
    public class Channel
    {
        public string ConnectionID { get; set; }

        public User User {get; set;}
    }
}