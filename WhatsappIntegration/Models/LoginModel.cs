using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsappIntegration.WebUI.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username Field is not Valid!")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Field is not Valid!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;

    }
}
