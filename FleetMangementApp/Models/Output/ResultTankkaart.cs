using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMangementApp.Models.Output
{
    public class ResultTankkaart
    {
        public int Id { get; set; }
        public string Kaartnummer { get; set; }
        public string Pincode { get; set; }
        public string Geldigheidsdatum { get; set; }
        public bool IsGeblokkeerd { get; set; }
        public bool HeeftBestuurder { get; set; }

    }
}
