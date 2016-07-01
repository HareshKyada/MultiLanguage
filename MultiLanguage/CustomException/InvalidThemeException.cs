using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLanguage.CustomException
{
    public class InvalidThemeException : Exception
    {
        public InvalidThemeException()
        {

        }

        public InvalidThemeException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {

        }
    }
}