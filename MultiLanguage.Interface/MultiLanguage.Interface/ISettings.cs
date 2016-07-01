using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLanguage.Interface
{
    public interface ISettings
    {
        string CompanyName { get; set; }
        string Theme { get; set; }
        string DomainName { get; set; }

        string DisplayName { get; set; }
        string SmtpFromAddress { get; set; }
        string SmtpHost { get; set; }
        string SmtpPassword { get; set; }
        int SmtpPort { get; set; }
        bool EnableSsl { get; set; }
    }
}
