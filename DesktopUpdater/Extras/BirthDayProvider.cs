using DesktopUpdater.Interfaces;
using System.Text;

namespace DesktopUpdater.Extras;

public class BirthDayProvider : IBirthDayProvider
{
    private const string BirthdaysTxt = "birthdays.txt";
    private readonly Dictionary<string, string> birthdays = new();
    private readonly IDateToStringFormat dateToStringFormat;

    public BirthDayProvider(IDateToStringFormat dateToStringFormat)
    {
        this.dateToStringFormat = dateToStringFormat;
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
        Append(result, "Tegnap {0} születésnapja volt.", GetBirthDay(date.AddDays(-1)));
        Append(result, "Ma {0} születésnapja van.", GetBirthDay(date));
        Append(result, "Holnap {0} születésnapja lesz.", GetBirthDay(date.AddDays(1)));
        return result.ToString();
    }

    private void CreateEmptyBirthDaysFile(string filePath)
    {
        using var writer = File.CreateText(filePath);
        var dayCountInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        for (int month = 0; month < dayCountInMonth.Length; month++)
        {
            for (int day = 1; day <= dayCountInMonth[month]; day++)
            {
                var line = dateToStringFormat.GetStringFormat(month + 1, day);
                if ((month == 11) && (day == 31))
                {
                    writer.Write(line);
                }
                else
                {
                    writer.WriteLine(line);
                    if ((month == 1) && (day == 28))
                    {
                        line = dateToStringFormat.GetStringFormat(2, 29);
                        writer.WriteLine(line);
                    }
                }
            }
        }
        writer.Close();
    }

    private string GetBirthDay(DateTime date)
    {
        var dateKey = dateToStringFormat.GetStringFormat(date.Month, date.Day);
        if (birthdays.ContainsKey(dateKey))
        {
            return birthdays[dateKey].Trim();
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
