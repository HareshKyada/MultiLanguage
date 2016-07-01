
using System.Web.Mvc;
using MultiLanguage.Domain.Concrete;
using MultiLanguage.Infrastructure;
using MultiLanguage.Models;
namespace MultiLanguage.User.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult AccessDeniedPartial()
        {
            return View();
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult Message()
        {
            var errType = Request.QueryString["err"];

            var ErrorViewModel = new ErrorViewModel 
            {
                ErrorCode = errType,
                ErrorDescription = "An unknown error has occurred",
                Title=Settings.CompanyName
            };
            Server.ClearError();
            return View("Message", ErrorViewModel);

        }
        public ActionResult AjaxMessage()
        {
            var errType = Request.QueryString["err"];

            var ErrorViewModel = new ErrorViewModel
            {
                ErrorCode = errType,
                ErrorDescription = "an error occurred while processing your request",
                Title = Settings.CompanyName
            };
            Server.ClearError();
            return View("AjaxMessage", ErrorViewModel);

        }
        public ActionResult SessionTimeOut()
        {
            Server.ClearError();
            return View("SessionTimeOut");

        }

        public ActionResult Offline()
        {
            return RedirectToAction("Offline");
        }
    }
}