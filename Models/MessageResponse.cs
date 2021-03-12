using System;

namespace simple_messenger_backend.Models
{
    public class MessageResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public string MessageText { get; set; }
        public DateTime SendTime { get; set; }
    }
}