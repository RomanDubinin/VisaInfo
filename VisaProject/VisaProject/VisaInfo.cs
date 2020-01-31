using System;
using System.Linq;

namespace VisaProject
{
    public struct VisaInfo
    {
        public VisaResult Result { get; }
        public string StatementNumber { get; }
        public DateTime StatementDate { get; }
        public string City { get => StatementNumber.Substring(0, 4); }

        public VisaInfo(VisaResult result, string statementNumber, DateTime statementDate)
        {
            Result = result;
            StatementNumber = statementNumber;
            StatementDate = statementDate;
        }

        public VisaInfo(string stringValue)
        {
            var properties = stringValue.Split(" ");

            StatementDate = DateTime.Parse(properties[0]);
            StatementNumber = properties[1];

            if (Enum.TryParse(properties[2], out VisaResult result))
                Result = result;
            else
                throw new Exception($"parse error on: {stringValue}");
        }

        public override string ToString()
        {
            return $"{StatementDate:yyyy.MM.dd} {StatementNumber} {Result}";
        }
    }
}