using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Web.Mvc;
using MultiLanguage.Domain.Concrete;

namespace MultiLanguage.Infrastructure
{
    public class ErrorLogger
    {

        private readonly Exception _exception;
        private readonly ExceptionContext _exceptionContext;

        public ErrorLogger(Exception exception)
        {
            _exception = exception;
        }
        public ErrorLogger(Exception exception, ExceptionContext exceptionContext)
        {
            _exception = exception;
            _exceptionContext = exceptionContext;
        }

        public void Log()
        {
            bool isLogError = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["RCI.Common.LogError"]);
            if (isLogError)
            {
                LogError();
            }
            bool isMailSend = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["RCI.Common.IsErrorMailSend"]);

            SendEmail();

        }

        /// <summary>
        /// Sends an email if enabled in  settings. It expects an email address in  settings and also an smtp server
        /// If they are invalid, it fails silently
        /// </summary>
        private void SendEmail()
        {
            try
            {
                //MyBaseController b=new MyBaseController();
                string messageText = _exception.StackTrace.ToString();
                string template =
                    System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/HtmlPage/error.html"));


                //Get a StackTrace object for the exception
                StackTrace st = new StackTrace(_exception, true);

                //Get the first stack frame
                StackFrame frame = st.GetFrame(0);

                //Get the file name
                string fileName = frame.GetFileName();

                //Get the method name
                string methodName = frame.GetMethod().Name;

                //Get the line number from the stack frame
                int line = frame.GetFileLineNumber();

                //Get the column number
                int col = frame.GetFileColumnNumber();
                if (_exceptionContext != null)
                {
                    var s = System.Web.HttpContext.Current.Request.UrlReferrer;
                    if (s != null)
                    {
                        string OriginalString = s.OriginalString;
                        template = template.Replace("#PreviousPage#", OriginalString);
                    }
                    else
                    {
                        template = template.Replace("#PreviousPage#", "");
                    }

                    string action = _exceptionContext.RouteData.Values["action"].ToString();
                    string controller = _exceptionContext.RouteData.Values["controller"].ToString();

                }


                //template = template.Replace("#ErrorCode#", _exception.Source);
                template = template.Replace("#ErrorLink#", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                template = template.Replace("#PathAndQuery#", System.Web.HttpContext.Current.Request.Url.PathAndQuery);

                template = template.Replace("#FileaName#", fileName);
                template = template.Replace("#MethodName#", methodName);
                template = template.Replace("#LineAndColumn#", string.Format("Line:{0},Column:{1}", line, col));


                template = template.Replace("#ExceptionMessage#", _exception.Message);
                template = template.Replace("#ErrorDesc#", messageText);

                Emailer.SendMessage(template);
            }
#pragma warning disable 0168
            catch (Exception ex)
#pragma warning disable 0168
            {
                //throw ex;
            }
        }

        private void LogError()
        {
            try
            {
                //var errorEntity = _exception.ToApplicationErrorEntity();
                //log error in databse
            }
            catch (Exception ex)
            {
            }
        }

        public void SendMail(string Subject, string Message)
        {
            try
            {
                string messageText = _exception.StackTrace.ToString();
                string template =
                    System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/HtmlPage/error.html"));


                //Get a StackTrace object for the exception
                StackTrace st = new StackTrace(_exception, true);

                //Get the first stack frame
                StackFrame frame = st.GetFrame(0);

                //Get the file name
                string fileName = frame.GetFileName();

                //Get the method name
                string methodName = frame.GetMethod().Name;

                //Get the line number from the stack frame
                int line = frame.GetFileLineNumber();

                //Get the column number
                int col = frame.GetFileColumnNumber();
                if (_exceptionContext != null)
                {
                    var s = System.Web.HttpContext.Current.Request.UrlReferrer;
                    if (s != null)
                    {
                        string OriginalString = s.OriginalString;
                        template = template.Replace("#PreviousPage#", OriginalString);
                    }
                    else
                    {
                        template = template.Replace("#PreviousPage#", "");
                    }

                    string action = _exceptionContext.RouteData.Values["action"].ToString();
                    string controller = _exceptionContext.RouteData.Values["controller"].ToString();

                }


                //template = template.Replace("#ErrorCode#", _exception.Source);
                template = template.Replace("#ErrorLink#", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                template = template.Replace("#PathAndQuery#", System.Web.HttpContext.Current.Request.Url.PathAndQuery);

                template = template.Replace("#FileaName#", fileName);
                template = template.Replace("#MethodName#", methodName);
                template = template.Replace("#LineAndColumn#", string.Format("Line:{0},Column:{1}", line, col));


                template = template.Replace("#ExceptionMessage#", _exception.Message);
                template = template.Replace("#ErrorDesc#", messageText);

                Emailer.SendMessage(Settings.SmtpFromAddress, "Error occurs", template);
            }
#pragma warning disable 0168
            catch (Exception ex)
#pragma warning disable 0168
            {
                //throw ex;
            }



        }
    }
}
