using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiLanguage.HtmlHelpers
{
    public static class LanguageListDropDownHelper
    {
        public static MvcHtmlString DropdownForLanguage(this HtmlHelper helper, string name)
        {
            return DropdownForLanguage(helper, name, null);
        }
       
        public static MvcHtmlString DropdownForLanguage(this HtmlHelper helper, string name,object htmlAttributes, bool IsDesc = false)
        {
            string psize = string.Empty;
            var routeValues = new RouteValueDictionary();
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("function Navigate() {");
            sb.Append("window.location.href = document.getElementById('" + name + "').value;");
            sb.Append("}");
            sb.Append("</script>");


            TagBuilder dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("name", name);
            dropdown.Attributes.Add("id", name);
            dropdown.Attributes.Add("onchange", "Navigate()");


            StringBuilder options = new StringBuilder();
            string[,] LanguageList = new string[,] { { "en-US", "[English]", "English" }, { "zh-CN", "[Chinese]", "Chinese" }, { "fr-FR", "[French]", "French" } };

            options = options.Append("<option  value=''>--select--</option>");
            for (int i = 0; i < (LanguageList.Length / 3); i++)
            {
                string cultureName = LanguageList[i, 0];
                string selectedText = LanguageList[i, 1];
                string unselectedText = LanguageList[i, 2];

                string languageRouteName = "lang";
                bool strictSelected = false;

                var language = helper.LanguageUrl(cultureName, languageRouteName, strictSelected);
                if (language.IsSelected)
                {
                    options = options.Append("<option selected=true  value='" + language.Url.ToString() + "'>" + LanguageList[i, 2].ToString() + "</option>");
                }
                else
                {
                    options = options.Append("<option  value='" + language.Url.ToString() + "'>" + LanguageList[i, 2].ToString() + "</option>");
                }
            }
            dropdown.InnerHtml = options.ToString();
            dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal) + sb.ToString());
        }
    }
}