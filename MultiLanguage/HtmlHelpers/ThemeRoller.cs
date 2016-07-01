using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MultiLanguage.Mapper;
using MultiLanguage.Extension;

namespace MultiLanguage.HtmlHelpers
{
    public static class ThemeRoller
    {
        private const string ThemeCssBasePath = "~/Contents/Themes/{0}/css";
        private const string ThemeScriptBasePath = "~/Contents/Themes/{0}/js";

        public static MvcHtmlString LoadStylesAndScripts(this HtmlHelper helper, string themeName)
        {
            
            var urlHelper = helper.GetUrlHelper();
            var builder = new StringBuilder();

            var pathMapper = new PathMapper();
            var basePath = pathMapper.MapPath(string.Format(ThemeCssBasePath, themeName));

            var cssFiles = Directory.GetFiles(basePath, "*.css").ToList();
           
            cssFiles.ForEach(file =>
            {
                var themeStyle=BuildCss(urlHelper,
                    string.Format("{0}/{1}", string.Format(ThemeCssBasePath, themeName), Path.GetFileName(file)));
                //var themeStyle = new TagBuilder("link");
                //themeStyle.MergeAttribute("href", urlHelper.Content(string.Format("{0}/{1}", string.Format(ThemeCssBasePath, themeName), Path.GetFileName(file))));
                //themeStyle.MergeAttribute("rel", "stylesheet");
                //themeStyle.MergeAttribute("type", "text/css");

                builder.AppendLine(themeStyle.ToString());
            });

            basePath = pathMapper.MapPath(string.Format(ThemeScriptBasePath, themeName));

            if (Directory.Exists(basePath))
            {
                var scriptFiles = Directory.GetFiles(basePath, "*.js").ToList();

                scriptFiles.ForEach(file =>
                {
                    var themeScript = new TagBuilder("script");
                    themeScript.MergeAttribute("src", urlHelper.Content(string.Format("{0}/{1}", string.Format(ThemeScriptBasePath, themeName), Path.GetFileName(file))));
                    themeScript.MergeAttribute("type", "text/javascript");

                    builder.AppendLine(themeScript.ToString());
                });
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        /// <summary>
        /// The layout pages might require custom script content that might not be part
        /// of the .js file's or properties needed by the layout pages (like the site's root url).
        /// </summary>
        /// <param name="urlHelper">The Url helper.</param>
        /// <returns>The dynamic javascript content</returns>
        private static string InjectScript(UrlHelper urlHelper)
        {
            var baseUrl = urlHelper.Content("~/");
            const string injectScriptFormat = @"<script type=""text/javascript"" language=""javascript"">{0}</script>";
            return string.Format(injectScriptFormat, "var siteRoot='" + baseUrl + "';");
        }

        private static List<GenericFile> GetCustomFiles(bool syntaxHighlighterStatus)
        {
            var customFiles = new List<GenericFile>();

            if (syntaxHighlighterStatus)
                customFiles.Add(new GenericFile { FileRelativePath = "~/Scripts/SyntaxHighlighter.js", IsScript = true });

            return customFiles;
        }

        private static TagBuilder BuildCss(UrlHelper urlHelper, string relativePath)
        {
            var tag = new TagBuilder("link");
            tag.MergeAttribute("rel", "stylesheet");
            tag.MergeAttribute("type", "text/css");
            tag.MergeAttribute("href", urlHelper.Content(relativePath));

            return tag;
        }

        private static TagBuilder BuildScript(UrlHelper urlHelper, string relativePath)
        {
            var tag = new TagBuilder("script");
            tag.MergeAttribute("type", "text/javascript");
            tag.MergeAttribute("src", urlHelper.Content(relativePath));

            return tag;
        }

        private static List<GenericFile> GetFiles()
        {
            var genericFiles = new List<GenericFile>
            {
                
                new GenericFile { FileRelativePath = "~/Scripts/jquery-1.7.1.js", IsScript = true },
                new GenericFile { FileRelativePath = "~/Contents/Site.css", IsScript = false }
            };

            return genericFiles;
        }

        private class GenericFile
        {
            public string FileRelativePath { get; set; }
            public bool IsScript { get; set; }
        }
    
    }
}