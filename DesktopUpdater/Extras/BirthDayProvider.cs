using DesktopUpdater.Interfaces;
using System.Text;

namespace DesktopUpdater.Extras;

public class BirthDayProvider : IBirthDayProvider
{
    private const string BirthdaysTxt = "birthdays.txt";
    private readonly Dictionary<string, string> birthdays = new();

    public BirthDayProvider()
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, BirthdaysTxt);
        if (!File.Exists(filePath))
        {
            CreateEmptyBirthDaysFile(filePath);
        }
        var birthdaysFileContent = FileUtils.GetFileContent(filePath);
        var lines = birthdaysFileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        foreach (var line in lines)
        {
            var indexOfColon = line.IndexOf(":");
            if (indexOfColon > -1)
            {
                var nameStart = indexOfColon + 2;
                birthdays.Add(line[..nameStart], line[nameStart..]);
            }
        }
    }

    public string GetBirthDayText(DateTime date)
    {
        var result = new StringBuilder();
        AddDay(date.AddDays(-1), result, "Tegnap {0} születésnapja volt.");
        AddDay(date, result, "Ma {0} születésnapja van.");
        AddDay(date.AddDays(1), result, "Holnap {0} születésnapja lesz.");
        return result.ToString();
    }

    private void AddDay(DateTime date, StringBuilder result, string formatString)
    {
        var day = GetBirthDay(date);
        if (!String.IsNullOrEmpty(day))
        {
            Append(result, formatString, day);
        }
    }

    private static void CreateEmptyBirthDaysFile(string filePath)
    {
        using var writer = File.CreateText(filePath);
        var dayCountInMonth = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        for (int monthIndex = 0; monthIndex < dayCountInMonth.Length; monthIndex++)
        {
            for (int day = 1; day <= dayCountInMonth[monthIndex]; day++)
            {
                var line = $"{NumberToMonthConverter.Convert(monthIndex)} {day}: ";

                if ((monthIndex == 11) && (day == 31))
                {
                    writer.Write(line);
                }
                else
                {
                    writer.WriteLine(line);
                }
            }
        }
        writer.Close();
    }

    private string GetBirthDay(DateTime date)
    {
        var dateKey = $"{NumberToMonthConverter.Convert(date.Month - 1)} {date.Day}: ";
        if (birthdays.TryGetValue(dateKey, out string? value))
        {
            if (String.IsNullOrEmpty(value))
            {
                return String.Empty;
            }
            return String.Concat(dateKey, value.Trim());
        }
        return String.Concat("Date not found:", dateKey);
    }

    private static void Append(StringBuilder stringBuilder, string formatString, string element)
    {
        if (element != String.Empty)
        {
            if (stringBuilder.ToString() != String.Empty)
            {
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendFormat(formatString, element);
        }
    }
}
