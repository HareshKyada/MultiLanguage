using System.Web.Mvc;
using System.Web.Optimization;
using MultiLanguage.Attribute;

namespace MultiLanguage.Areas
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterBundles(BundleTable.Bundles);
            context.MapRoute("Admindefault", "Admin/{controller}/{action}/{id}",
               new
               {
                   controller = "Admin",
                   action = "Login",
                   id = UrlParameter.Optional
               },
               namespaces: new string[] { "MultiLanguage.Admin.Controllers" }
           );


            context.MapRoute("Localization", "{lang}/Admin/{controller}/{action}/{id}",
              new
              {
                  lang = "en-US",
                  controller = "Admin",
                  action = "Login",
                  id = UrlParameter.Optional
              },
              namespaces: new string[] { "MultiLanguage.Admin.Controllers" }
          );
            
        }

        private void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/admin/bundles/scripts")
            //    .Include("~/Scripts/jquery-1.7.1.js")
            //    .Include("~/Scripts/jquery.unobtrusive*")
            //    .Include("~/Scripts/jquery.validate*")
            //    );
            //bundles.Add(new StyleBundle("~/admin/bundles/style")
            //    .Include("~/Contents/Site.css")
            //    );
        }
    }
}
