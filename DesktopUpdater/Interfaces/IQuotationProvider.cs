namespace DesktopUpdater.Interfaces;

public interface IQuotationProvider
{
    string GetQuotation();

    string GetQuotation(int index);
}
