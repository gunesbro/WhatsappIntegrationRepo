using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhatsappIntegration.Entity.Concrete
{
    /// <summary>
    /// Master record for Conversation
    /// </summary>
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public string PhoneNumber { get; set; }
        public bool SmartReplyState { get; set; }
        public DateTime CreationDate { get; set; }
        public int ChatTypeId { get; set; } // Is it whatsapp chat or sms or etc... -ChatTypes Table
        public int CompanyId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
