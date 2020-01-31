using System;
using System.Globalization;
using System.Text.RegularExpressions;
using VisaProject;

namespace DataContainer
{
    public class ActualityDateProvider : IActualityDateProvider
    {
        private readonly string regex = "(?:_)([0-9.]+)(?:\\.txt)";
        private readonly string dateFormat = "yyyy.MM.dd.hh.mm.ss";
        private readonly IFileNameFinder fileNameFinder;

        public ActualityDateProvider(IFileNameFinder fileNameFinder)
        {
            this.fileNameFinder = fileNameFinder;
        }

        public DateTime GetActualityDate(string subDirectory)
        {
            var fileName = fileNameFinder.FindName(subDirectory);
            var dateString = Regex.Match(fileName, regex).Groups[1].Value;
            return DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
        }
    }
}