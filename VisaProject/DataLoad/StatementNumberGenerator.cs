using System;

namespace DataLoad
{
    public class StatementNumberGenerator
    {
        public string Generate(string city, DateTime date, int number)
        {
            return $"{city}{date:yyyMMdd}{number.ToString().PadLeft(4, '0')}";
        }
    }
}