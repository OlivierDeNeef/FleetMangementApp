using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMangementApp.Models.Output
{
    public class ResultVoertuig
    {
        public int Id { get; set; }
        public string Merk { get; set; }
        public string Model { get; set; }
        public string Nummerplaat { get; set; }
        public string Chassisnummer { get; set; }
        public string WagenType { get; set; }
        public string Brandstof { get; set; }
        public bool HeeftBestuurder { get; set; }
    }
}
