namespace DesktopUpdater.Extras;

public static class NumberToMonthConverter
{
    private const string January = "január";
    private const string February = "február";
    private const string March = "március";
    private const string April = "április";
    private const string May = "május";
    private const string June = "június";
    private const string July = "július";
    private const string August = "augusztus";
    private const string September = "szeptember";
    private const string October = "október";
    private const string November = "november";
    private const string December = "december";

    public static string Convert(int number)
    {
        return number switch
        {
            0 => January,
            1 => February,
            2 => March,
            3 => April,
            4 => May,
            5 => June,
            6 => July,
            7 => August,
            8 => September,
            9 => October,
            10 => November,
            11 => December,
            _ => String.Empty
        };
    }
}
