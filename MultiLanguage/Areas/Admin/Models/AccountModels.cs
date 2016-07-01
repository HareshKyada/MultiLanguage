using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiLanguage.Admin.Models
{

   
    public class LoginModel
    {
        [Display(Name = "UserName", ResourceType = typeof(LanguageResource.login))]
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof(LanguageResource.login))]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(LanguageResource.login))]
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(LanguageResource.login))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(LanguageResource.login))]
        public bool RememberMe { get; set; }
    }

  
}
