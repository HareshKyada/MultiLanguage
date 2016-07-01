using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLanguage.Interface
{
    public interface IPathMapper
    {
        string MapPath(string relativePath);
    }
}
