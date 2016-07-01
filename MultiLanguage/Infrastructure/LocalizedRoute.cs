using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiLanguage.Infrastructure
{
    public class LocalizedRoute : Route
    {
        public LocalizedRoute()
            : base(
                "{lang}/{controller}/{action}/{id}",
                new RouteValueDictionary(new
                {
                    lang = "en-US",
                    controller = "home",
                    action = "index",
                    id = UrlParameter.Optional
                }),
                new RouteValueDictionary(new
                {
                    lang = @"[a-z]{2}-[a-z]{2}"
                }),
                new MvcRouteHandler()
            )
        {

           
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var rd = base.GetRouteData(httpContext);
            if (rd == null)
            {
                return null;
            }

            var lang = rd.Values["lang"] as string;
            if (string.IsNullOrEmpty(lang))
            {
                // pick a default culture
                lang = "en-US";
            }

            var culture = new CultureInfo(lang);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            return rd;
        }
    }
}