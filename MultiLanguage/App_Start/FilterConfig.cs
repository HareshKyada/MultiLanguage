using System.Web;
using System.Web.Mvc;
using MultiLanguage.Attribute;

namespace MultiLanguage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorAttribute());
            //filters.Add(new HandleErrorAttribute());
        }
    }
}