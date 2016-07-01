#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion

using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using MultiLanguage.Domain.Utilities;
using MultiLanguage.Interface;

namespace MultiLanguage.Infrastructure
{
    public static class Emailer
    {

        private static string ErrorEmail
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["Error.ToMail"].ToString();
                }
                catch 
                {
                    return string.Empty;
                }
            }
        }
        private static string ErrorSubject
        {
            get
            {
                try
                {
                    return "Error in --" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    return "Error in ";
                }
            }
        }
        private static string SiteSmtpPassword
        {
            get
            {
                
                var smtpPassword = ConfigurationManager.AppSettings["Error.SMTPPassword"].ToString();
                if (!string.IsNullOrEmpty(smtpPassword))
                {
                    smtpPassword = AESEncryption.Decrypt(smtpPassword);
                }
                return smtpPassword;
            }
        }

        private static string SiteSmtpAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["Common.SMTPServerName"].ToString();
            }
        }
        private static string SiteFromAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["Error.FromEmail"].ToString();
            }
        }

        public static bool SendMessage(string emailMessage)
        {
            return SendMessage(SiteFromAddress, SiteFromAddress, ErrorSubject, emailMessage);
        }
        public static bool SendMessage(string subjectMsg, string emailMessage)
        {
            return SendMessage(SiteFromAddress, SiteFromAddress, subjectMsg, emailMessage);
        }

        public static bool SendMessage(string toAddress, string subjectMsg, string emailMessage)
        {
            return SendMessage(SiteFromAddress, toAddress, subjectMsg, emailMessage);
        }

        public static bool SendMessage(string fromAddress, string toAddress, string subjectMsg, string emailMessage)
        {
            try
            {
                var message = new MailMessage {From = new MailAddress(fromAddress)};
                message.To.Add(new MailAddress(toAddress));
                message.Subject = subjectMsg;
                message.IsBodyHtml = true;
                message.Body = emailMessage;

                var smtpClient = new SmtpClient(SiteSmtpAddress);

                var smtpPassword = SiteSmtpPassword;
                if (!string.IsNullOrEmpty(smtpPassword))
                {
                    smtpClient.Credentials = new NetworkCredential(fromAddress, smtpPassword);
                }

                smtpClient.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }    
        
    }
}