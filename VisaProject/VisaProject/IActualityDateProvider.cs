using System;

namespace VisaProject
{
    public interface IActualityDateProvider
    {
        DateTime GetActualityDate(string city);
    }
}