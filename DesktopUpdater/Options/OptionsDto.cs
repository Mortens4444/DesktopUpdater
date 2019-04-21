using System.Drawing;

namespace DesktopUpdater.Options
{
    public class OptionsDto
    {
        public byte NumberOfAttemptsToDownloadBackground { get; set; } = 3;

        public ushort WaitBetweenDownloadAttemptsInMiliseconds { get; set; } = 3000;

        public ushort FixQuotationNumber1 { get; set; } = 1;

        public ushort FixQuotationNumber2 { get; set; } = 2;

        public bool ShowQuotation { get; set; } = true;

        public bool ShowBirthdays { get; set; } = true;

        public bool ShowNamedays { get; set; } = true;

        public bool ShowImageInfo { get; set; } = true;
        
        public bool ForceDownloadImage { get; set; } = false;

        public bool ForceCustomSize { get; set; } = false;

        public ushort Width { get; set; } = 1920;

        public ushort Height { get; set; } = 1080;
    }
}
