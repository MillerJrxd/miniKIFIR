using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniKIFIR_UI
{
    public interface IFelvetelizok
    {
        String OM_Azonosito { get; set; }
        String Neve { get; set; }
        String Email { get; set; }
        DateTime SzuletesiDatum { get; set; }
        String ErtesitesiCime { get; set; }
        int Matematika { get; set; }
        int Magyar { get; set; }

        String CSVSortAdVissza();

        void ModositCSVSorral(String csvString);
    }
}
