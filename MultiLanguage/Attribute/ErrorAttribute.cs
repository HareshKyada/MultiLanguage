using System;
using System.Web.Mvc;
using System.Web.Routing;
using MultiLanguage.CustomException;
using MultiLanguage.Infrastructure;

namespace MultiLanguage.Attribute
{
    public class ErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Called when an exception occurs.
        /// 
        /// Depending on the type of exception, user is redirected to a corresponding page
        /// 
        ///     * UrlNotFoundException -> 404 page
        ///     * Any other exception -> a generic error page
        /// </summary>
        /// <param name="filterContext">The action-filter context.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public override void OnException(ExceptionContext filterContext)
        {
            var errType = string.Empty;
            var exception = filterContext.Exception;
            var errorLogger = new ErrorLogger(exception,filterContext);
            errorLogger.Log();

            filterContext.ExceptionHandled = true;
            filterContext.Result = GetRedirectResultByExceptionType(errType, filterContext);



            base.OnException(filterContext);
        }

        private static ActionResult GetRedirectResultByExceptionType(string errType, ExceptionContext exceptionContext)
        {
            var urlHelper = new UrlHelper(exceptionContext.HttpContext.Request.RequestContext);
            var exception = exceptionContext.Exception;
            if (exception is UnauthorizedAccessException)
                errType = "unauthorized";
            else if (exception is InvalidMonthException)
                errType = "invalid-month";

            ActionResult redirectTo =  new RedirectResult(urlHelper.Action("Message", "Error", new { Area = "", err = errType }));

            if (exception is UrlNotFoundException)
                redirectTo = new RedirectToRouteResult("Error404", new RouteValueDictionary());

            if (exception is InvalidThemeException)
                redirectTo = new RedirectToRouteResult("InvalidTheme", new RouteValueDictionary());

            if (exceptionContext.HttpContext.Request.IsAjaxRequest())
            {
                redirectTo = new RedirectToRouteResult(
                                         new RouteValueDictionary(
                                             new
                                             {
                                                 controller = "Error",
                                                 action = "AjaxMessage"
                                             })
                                         );
            }
            else
            {

                redirectTo = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Error", action = "Message" }));
            }
            return redirectTo;
        }
    }
}