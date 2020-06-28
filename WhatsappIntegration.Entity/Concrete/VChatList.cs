using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhatsappIntegration.Entity.Concrete
{
    public class VChatList
    {
        public int ChatId { get; set; }
        public int ChatMessageId { get; set; }
        public int CompanyId { get; set; }
        public string PhoneNumber { get; set; }
        public bool SmartReplyState { get; set; }
        public string ChatTypeName { get; set; }
        public string LastMessage { get; set; }
        public string Person { get; set; }
        [NotMapped]
        public int UnreadMessageCount { get; set; }
    }
}
