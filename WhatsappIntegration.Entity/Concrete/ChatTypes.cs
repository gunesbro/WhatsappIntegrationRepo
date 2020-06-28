using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhatsappIntegration.Entity.Concrete
{
    /// <summary>
    /// If you want to add custom Chat Type for specific company you can add this table
    /// </summary>
    public class ChatTypes
    {
        [Key]
        public int ChatTypeId { get; set; }
        public string ChatTypeName { get; set; }
        public string ChatTypeIdentity { get; set; }
        public int CompanyId { get; set; }
        public string ChatType { get; set; }
    }
}
