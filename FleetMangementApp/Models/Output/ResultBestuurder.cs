using System;

namespace FleetMangementApp.Models.Output
{
    public class ResultBestuurder
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Geboortedatum { get; set; }
        public bool HeeftVoertuig { get; set; }
        public bool HeeftTankkaart { get; set; }
        
    }
}