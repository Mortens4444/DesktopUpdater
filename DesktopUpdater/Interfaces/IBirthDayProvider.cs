using System;

namespace DesktopUpdater.Interfaces
{
    public interface IBirthDayProvider
    {
        string GetBirthDayText(DateTime date);
    }
}
