using System;
using System.Text.RegularExpressions;
using VisaProject;

namespace DataLoad
{
    public class VisaInfoParser
    {
        private string regexPattern = "(?:<span class=\"alert alert-[\\w]+\"><strong>)([\\wěščřžýáíéóúůďťňĎŇŤŠČŘŽÝÁÍÉÚŮ –-]+)(?:<\\/strong>)";

        public VisaResult Parse(string text)
        {
            var result = Regex.Match(text, regexPattern);
            return stringToVisaResult(result.Groups[1].Value);
        }

        private VisaResult stringToVisaResult(string str)
        {
            switch (str)
            {
                case "Nenalezeno":
                    return VisaResult.None;
                case "Zpracovává se":
                    return VisaResult.InService;
                case "Vyřízeno – NEPOVOLENO":
                    return VisaResult.Failure;
                case "Vyřízeno – POVOLENO":
                    return VisaResult.Success;
                default:
                    return VisaResult.None;
            }
        }
    }
}