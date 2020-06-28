using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhatsappIntegration.Entity.Concrete
{
    public class ChatMessages
    {
        [Key]
        public int ChatMessageId { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public int ChatId { get; set; }
        public bool MessageDirection { get; set; } //Client: true / User Agent: false
        public bool IsItRead { get; set; } // Is Message Read from user? If Not notify
        public int? AnsweringUserId { get; set; } // Who answered to client?
        public string TwilioMessageId { get; set; }
        public string TwilioMessageStatus { get; set; }

    }
}
