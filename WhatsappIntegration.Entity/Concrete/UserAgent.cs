using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsappIntegration.Entity.Concrete
{
    //User Agent IdentityCore dan inherit alacak
    public class UserAgent: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CompanyId { get; set; }
    }
}
