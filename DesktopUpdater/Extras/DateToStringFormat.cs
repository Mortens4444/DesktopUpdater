using DesktopUpdater.Interfaces;
using System;
using System.Globalization;

namespace DesktopUpdater.Extras
{
    public class DateToStringFormat : IDateToStringFormat
    {
        private readonly DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();

        public string GetStringFormat(int month, int day)
        {
            var monthName = dateTimeFormatInfo.GetMonthName(month);
            return String.Format($"{monthName} {day}: ");
        }
    }
}