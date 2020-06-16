using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhatsappIntegration.Entity.Concrete
{
    public class SmartReplies
    {
        [Key]
        public int SmartReplyId { get; set; }
        public string Keyword { get; set; }
        public string Answer { get; set; }
        public bool FullMatchOrContains { get; set; } // Is Keyword Full Match with Client's message or contains it?
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}
