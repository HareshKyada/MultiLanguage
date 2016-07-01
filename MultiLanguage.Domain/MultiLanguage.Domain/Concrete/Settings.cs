using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiLanguage.Interface;

namespace MultiLanguage.Domain.Concrete
{
    public static class Settings 
    {
        static string _domainName;
        public static string DomainName
        {
            get
            {
                return _domainName;
            }
            set
            {
                _domainName = value;
            }
        }

        public static string Theme { get; set; }
        public static string CompanyName { get; set; }

        public static string DisplayName { get; set; }
        public static string SmtpFromAddress { get; set; }
        public static string SmtpHost { get; set; }
        public static string SmtpPassword { get; set; }
        public static int SmtpPort { get; set; }
        public static bool EnableSsl { get; set; }
    }
}
