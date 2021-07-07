using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Common
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập user name")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}