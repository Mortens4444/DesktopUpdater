namespace DesktopUpdater;

public static class StringExtensions
{
    public static string Substring(this string value, string firstElement, string secondElement)
    {
        return value.Substring(firstElement, secondElement, false, 0);
    }

    public static string Substring(this string value, string firstElement, string secondElement, bool caseInsensitive, int startIndex)
    {
        var sIndex = caseInsensitive ? value.IndexOf(firstElement, startIndex, StringComparison.CurrentCultureIgnoreCase) : value.IndexOf(firstElement, startIndex);

        if (sIndex != -1)
        {
            sIndex += firstElement.Length;

            var eIndex = caseInsensitive ? value.IndexOf(secondElement, sIndex, StringComparison.CurrentCultureIgnoreCase) : value.IndexOf(secondElement, sIndex);

            return eIndex == -1 ? value[sIndex..] : value[sIndex..eIndex];
        }
        return String.Empty;
    }
}
