using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Web.Http.Controllers;
using System.Collections.Generic;
using MultiLanguage.Infrastructure;



namespace MultiLanguage.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {         

            
        }
    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (HttpContext.Current != null && HttpContext.Current.Session["UserSettings"] != null)
            {

                if (BaseController.UserSetting != -1)
                {
                    if (HttpContext.Current.Session["Offline"] != null)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary(
                                       new
                                       {
                                           controller = "Account",
                                           action = "Offline"
                                       })
                                   );
                    }
                    else
                    {

                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                     new RouteValueDictionary(
                                         new
                                         {
                                             controller = "Error",
                                             action = "AccessDeniedPartial"
                                         })
                                     );
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                     new RouteValueDictionary(
                                         new
                                         {
                                             controller = "Error",
                                             action = "AccessDenied"
                                         })
                                     );
                        }




                    }
                }
                else
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {

                        filterContext.HttpContext.Response.StatusCode = 401;
                        filterContext.Result = new JsonResult
                        {
                            Data = new
                            {
                                Error = "NotAuthorized",
                                LogOnUrl = "Account/Login"
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                        filterContext.HttpContext.Response.End();

                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                       new
                                       {
                                           controller = "Account",
                                           action = "Login"
                                       })
                                           );
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                               new
                               {
                                   controller = "Account",
                                   action = "Login"
                               })
                                   );
            }            
        }

        protected override bool AuthorizeCore(HttpContextBase Context)
        {

            if (Context == null)
                throw new ArgumentNullException("httpContext");


            if (HttpContext.Current==null || HttpContext.Current.Session["Offline"] != null)
            {
                return false;
            }
            if (HttpContext.Current.Session["UserSettings"] != null)
            {
               
                if (BaseController.UserSetting != -1)
                {
                    if (!string.IsNullOrEmpty(Users))
                    {
                        var PermittedUsers= Users.Split(Convert.ToChar(",")).Select(p => p.Trim()).ToList();
                        if (PermittedUsers.Contains(BaseController.UserSetting.ToString()))
                        {
                            return true;
                        }
                    }

                    
                    if (BaseController.UserSetting != -1)
                    {
                        string roleID = Convert.ToString(BaseController.UserSetting);
                        var PermittedRoles = Roles.Split(Convert.ToChar(",")).Select(p => p.Trim()).ToList(); 
                        
                        if (!PermittedRoles.Contains(roleID))
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }  

}