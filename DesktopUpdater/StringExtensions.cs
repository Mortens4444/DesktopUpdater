using Consts;
using System;

namespace DesktopUpdater
{
    public static class StringExtensions
    {
        public static string Substring(this string value, string firstElement, string secondElement)
        {
            return value.Substring(firstElement, secondElement, false, 0);
        }

        //public static string Substring(this string value, string firstElement, string secondElement, bool caseInsensitive)
        //{
        //    return value.Substring(firstElement, secondElement, caseInsensitive, 0);
        //}

        //public static string Substring(this string value, string firstElement, string secondElement, int startIndex)
        //{
        //    return value.Substring(firstElement, secondElement, false, startIndex);
        //}

        public static string Substring(this string value, string firstElement, string secondElement, bool caseInsensitive, int startIndex)
        {
            var sIndex = caseInsensitive ? value.IndexOf(firstElement, startIndex, StringComparison.CurrentCultureIgnoreCase) : value.IndexOf(firstElement, startIndex);

            if (sIndex != Constants.NOT_FOUND)
            {
                sIndex += firstElement.Length;

                var eIndex = caseInsensitive ? value.IndexOf(secondElement, sIndex, StringComparison.CurrentCultureIgnoreCase) : value.IndexOf(secondElement, sIndex);

                return eIndex == Constants.NOT_FOUND ? value.Substring(sIndex) : value.Substring(sIndex, eIndex - sIndex);
            }
            return String.Empty;
        }
    }
}
