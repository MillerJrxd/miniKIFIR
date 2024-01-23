using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniKIFIR_UI
{
    public class Adatok : IFelvetelizok
    {
        string szam;
        string nev;
        string email;
        DateTime szulDat;
        string lakcim;
        int matekEred;
        int magyarEred;

        public Adatok() { }
        public Adatok(string sor)
        {
            string[] beolv = sor.Split(";");

            OM_Azonosito = beolv[0];
            Neve = beolv[1];
            Email = beolv[2];
            SzuletesiDatum = DateTime.Parse(beolv[3]);
            ErtesitesiCime = beolv[4];
            Matematika = int.Parse(beolv[5]);
            Magyar = int.Parse(beolv[6]);
        }

        public Adatok(string szam, string nev, string email, DateTime szulDat, string lakcim, int matekEred, int magyarEred)
        {
            this.szam = szam;
            this.nev = nev;
            this.email = email;
            this.szulDat = szulDat;
            this.lakcim = lakcim;
            this.matekEred = matekEred;
            this.magyarEred = magyarEred;
        }

        public string OM_Azonosito { get => szam; set => szam = value; }
        public string Neve { get => nev; set => nev = value; }
        public string Email { get => email; set => email = value; }
        public DateTime SzuletesiDatum { get => szulDat; set => szulDat = value; }
        public string ErtesitesiCime { get => lakcim; set => lakcim = value; }
        public int Matematika { get => matekEred; set => matekEred = value; }
        public int Magyar { get => magyarEred; set => magyarEred = value; }

        public string CSVSortAdVissza()
        {
            throw new NotImplementedException();
        }

        public void ModositCSVSorral(string csvString)
        {
            throw new NotImplementedException();
        }
    }
}
