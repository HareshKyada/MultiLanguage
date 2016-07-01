using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLanguage.CustomException
{
    public class UrlNotFoundException : Exception
    {
        public UrlNotFoundException()
        {

        }

        public UrlNotFoundException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {

        }
    }
}