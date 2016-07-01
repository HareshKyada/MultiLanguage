using System.Web;
using MultiLanguage.Interface;

namespace MultiLanguage.Mapper
{
    public class PathMapper : IPathMapper
    {
        public string MapPath(string relativePath)
        {
            return HttpContext.Current
                              .Server
                              .MapPath(relativePath);
        }
    }
}