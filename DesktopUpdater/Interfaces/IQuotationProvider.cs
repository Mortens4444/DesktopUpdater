using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUpdater.Interfaces
{
    public interface IQuotationProvider
    {
        string GetQuotation();

        string GetQuotation(int index);
    }
}
