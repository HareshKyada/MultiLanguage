using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MultiLanguage.Extension;
namespace MultiLanguage.HtmlHelpers
{
    public static class ScriptStyleProvider
    {
        /// <summary>
        /// This html extension injects the following sets of files sequentially
        /// 
        ///     * Common css & js files like SiteCommons, jQuery
        ///     * Code highlighter styles
        ///     * Code highlighter scripts
        ///     * Injected script content
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns>a mvc html string w/ various scripts & styles</returns>
        public static MvcHtmlString GetCommonScriptsAndStyles(this HtmlHelper htmlHelper)
        {
            var urlHelper = htmlHelper.GetUrlHelper();
            //var settingsRepository = InstanceFactory.CreateSettingsInstance();
            var sBuilder = new StringBuilder();

            var customFiles = GetCustomFiles(false);

            GetFiles().ForEach(file =>
            {
                var tag = file.IsScript ? BuildScript(urlHelper, file.FileRelativePath)
                                               : BuildCss(urlHelper, file.FileRelativePath);
                sBuilder.AppendLine(tag.ToString());
            });

            if (customFiles.Any())
            {
                //var cssData = htmlHelper.GenerateStyles(settingsRepository.BlogSyntaxTheme);
                //sBuilder.AppendLine(cssData);

                //var scriptData = htmlHelper.GenerateScripts(settingsRepository.BlogSyntaxScripts);
                //sBuilder.AppendLine(scriptData);

                customFiles.ForEach(file =>
                {
                    var tag = file.IsScript ? BuildScript(urlHelper, file.FileRelativePath)
                                                   : BuildCss(urlHelper, file.FileRelativePath);
                    sBuilder.AppendLine(tag.ToString());
                });
            }

            //sBuilder.AppendLine(InjectScript(urlHelper));

            return MvcHtmlString.Create(sBuilder.ToString());
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
                new GenericFile { FileRelativePath = "~/Scripts/jquery.unobtrusive-ajax.min.js", IsScript = true },
                new GenericFile { FileRelativePath = "~/Scripts/jquery.validate.min.js", IsScript = true },
                new GenericFile { FileRelativePath = "~/Scripts/jquery.validate.unobtrusive.min.js", IsScript = true },                
                new GenericFile { FileRelativePath = "~/Scripts/common.js", IsScript = true },
                new GenericFile { FileRelativePath = "~/Scripts/tinymce/tiny_mce.js", IsScript = true },
                new GenericFile { FileRelativePath = "~/Contents/Site.css", IsScript = false },
                new GenericFile { FileRelativePath = "~/Contents/SiteCommon.css", IsScript = false },
                new GenericFile { FileRelativePath = "~/Contents/bootstrap.min.css", IsScript = false },
                new GenericFile { FileRelativePath = "~/Contents/bootstrap-theme.min.css", IsScript = false }
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