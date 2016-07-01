using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MultiLanguage.CustomException;
using MultiLanguage.Interface;
using MultiLanguage.Mapper;
using MultiLanguage.Models;
using MultiLanguage.Database;
using MultiLanguage.Domain.Concrete;

namespace MultiLanguage.Infrastructure
{
    public interface IControllerProperties
    {
        bool IsAdminController { get; set; }
    }
    public class BaseController : Controller
    {

        #region "UserSetting after Login"
        public static int UserSetting
        {
            get
            {
                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session["UserSettings"] != null)
                {
                    return (int)System.Web.HttpContext.Current.Session["UserSettings"];
                }
                return -1;
            }
        }
        #endregion

        ////public virtual ContentResult Test()
        //{
        //    return Content("string");
        //}

        #region "Private Variable And Properties And Constructor"
        protected string ExpectedMasterName = "_Layout";
        private readonly IPathMapper _pathMapper;
        public BaseController()
        {
            _pathMapper = new PathMapper();
        }
        #endregion

        #region "Execute Core"
        protected override void ExecuteCore()
        {
            if (RouteData.Values["lang"] != null &&
                !string.IsNullOrWhiteSpace(RouteData.Values["lang"].ToString()))
            {
                // set the culture from the route data (url)
                var lang = RouteData.Values["lang"].ToString();
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
            }
            else
            {
                // load the culture info from the cookie
                var cookie = HttpContext.Request.Cookies["CurrentUICulture"];
                var langHeader = string.Empty;
                if (cookie != null)
                {
                    // set the culture by the cookie content
                    langHeader = cookie.Value;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                else
                {
                    // set the culture by the location if not speicified
                    langHeader = HttpContext.Request.UserLanguages[0];
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                // set the lang value into route data
                RouteData.Values["lang"] = langHeader;
            }

            // save the location into cookie
            HttpCookie _cookie = new HttpCookie("CurrentUICulture", Thread.CurrentThread.CurrentUICulture.Name);
            _cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Response.SetCookie(_cookie);

            base.ExecuteCore();
        }
        #endregion

        public string GetBlogTheme()
        {
            //return SettingsRepository.BlogTheme;
            string themeName = "GreenForest";
            string host = Settings.DomainName;//HttpContext.Request.Url.Host;
            //string hostName = Company.GetCompany().Select(s => s == host).FirstOrDefault().ToString();
            switch (host.ToLower())
            {

                case "fr-fr.example.com":
                    Settings.CompanyName = "GreenForest";
                    themeName = "GreenForest";
                    break;
                case "en-us.example.com":
                    Settings.CompanyName = "HandCrafted";
                    themeName = "HandCrafted";
                    break;
                default:
                    Settings.CompanyName = "Rangam Infotech";
                    themeName = "HandCrafted";
                    break;

            }
            return themeName;
        }

        public ActionResult Logo()
        {
            var model = new LogoViewModel
            {
                CompanyName = Settings.DomainName,
                RootUrl = GetRootUrl()
            };
            return PartialView(model);
        }


        protected string GetRootUrl()
        {
            return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var action = filterContext.Result as ViewResult;

            if (action != null && !string.IsNullOrEmpty(ExpectedMasterName))
            {
                var themeName = GetBlogTheme();//";// SettingsRepository.BlogTheme;

                if (ThemeExists(themeName))
                {
                    action.MasterName = MasterExists(themeName) ? string.Format(LayoutFormat, themeName, ExpectedMasterName)
                                                                : string.Format(DefaultLayoutFormat, ExpectedMasterName);

                }
                else
                {
                    throw new InvalidThemeException("Invalid theme {0}", themeName);
                }
            }

            base.OnActionExecuted(filterContext);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            throw new UrlNotFoundException("Unable to find an action with the name specified: [{0}]", actionName);
        }

        private bool MasterExists(string themeName)
        {
            var requiredFile = string.Format("{0}\\{1}.cshtml", _pathMapper.MapPath(string.Format("~/Themes/{0}", themeName)), ExpectedMasterName);
            return System.IO.File.Exists(requiredFile);
        }

        private bool ThemeExists(string themeName)
        {
            //if(System.IO.Directory.Exists(Server.MapPath(string.Format("~/Themes/{0}", themeName))))
            //{
            //    return true;
            //}
            //return false;
            var requiredFolder = _pathMapper.MapPath(string.Format("~/Themes/{0}", themeName));
            return System.IO.Directory.Exists(requiredFolder);
        }

        private const string LayoutFormat = "~/Themes/{0}/{1}.cshtml";
        private const string DefaultLayoutFormat = "~/Views/Shared/{0}.cshtml";
    }
}
