using DesktopUpdater.Interfaces;
using System.Globalization;

namespace DesktopUpdater.Extras;

public class DateToStringFormat : IDateToStringFormat
{
    private readonly DateTimeFormatInfo dateTimeFormatInfo = new();

    public string GetStringFormat(int month, int day)
    {
        var monthName = dateTimeFormatInfo.GetMonthName(month);
        return $"{monthName} {day}: ";
    }
}