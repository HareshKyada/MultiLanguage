using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLanguage.Database
{
    public static class Company
    {
        public static List<string> GetCompany()
        {
            List<string> lstCompany = new List<string>();
            lstCompany.Add("sp.localhost");
            lstCompany.Add("talon.localhost");
            return lstCompany;
        }
    }
}
