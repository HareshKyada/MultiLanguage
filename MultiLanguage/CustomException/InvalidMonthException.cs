using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLanguage.CustomException
{
    public class InvalidMonthException : Exception
    {
        public InvalidMonthException()
        {

        }

        public InvalidMonthException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {

        }

    }
}