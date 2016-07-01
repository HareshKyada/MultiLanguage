using System.Web.Mvc;
using System.Web.Optimization;

namespace MultiLanguage.Areas
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Userdefault",
                "User/{controller}/{action}/{id}",
                new
                {
                    controller = "User",
                    action = "Login",
                    id = UrlParameter.Optional
                },
                namespaces: new string[] { "MultiLanguage.User.Controllers" }
            );
            context.MapRoute("User_default", "{lang}/User/{controller}/{action}/{id}",
               new
               {
                   lang = "en-US",
                   controller = "User",
                   action = "Login",
                   id = UrlParameter.Optional
               },
             namespaces: new string[] { "MultiLanguage.User.Controllers" });
            RegisterBundles(BundleTable.Bundles);
        }
        private void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/user/bundles/scripts")
            //    .Include("~/Scripts/jquery-1.7.1.js")
            //    );
            //bundles.Add(new StyleBundle("~/user/bundles/style")
            //    .Include("~/Contents/Site.css")
            //    );
        }
    }
}
