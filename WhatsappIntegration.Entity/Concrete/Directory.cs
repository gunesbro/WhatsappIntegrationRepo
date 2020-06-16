using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhatsappIntegration.Entity.Concrete
{
    /// <summary>
    /// Telefon Rehberi gibi düşünülebilir. Kişilerin kayıtlarını tutma amaçlı.
    /// You can think this table like Telephone Directory.
    /// </summary>
    public class Directory
    {
        [Key]
        public int DirectoryId { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorUser { get; set; }
        public int CompanyId { get; set; }
        public bool IsDeleted { get; set; }


    }
}
