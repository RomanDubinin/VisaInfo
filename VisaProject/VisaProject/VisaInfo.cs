using System;
using System.Linq;

namespace VisaProject
{
    public struct VisaInfo
    {
        public VisaResult Result { get; }
        public string StatementNumber { get; }
        public DateTime StatementDate { get; }
        public string City { get; }

        public VisaInfo(string city, VisaResult result, string statementNumber, DateTime statementDate)
        {
            Result = result;
            StatementNumber = statementNumber;
            StatementDate = statementDate;
            City = city;
        }

        public VisaInfo(string stringValue)
        {
            var properties = stringValue.Split(" ");
            City = properties[0];
            StatementDate = DateTime.Parse(properties[1]);
            StatementNumber = properties[2];

            if (Enum.TryParse(properties[3], out VisaResult result))
                Result = result;
            else
                throw new Exception($"parse error on: {stringValue}");
        }

        public override string ToString()
        {
            return $"{City} {StatementDate:yyyy.MM.dd} {StatementNumber} {Result}";
        }
    }
}