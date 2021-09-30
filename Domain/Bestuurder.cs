using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace DomainLayer
{
    public class Bestuurder
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        private readonly List<RijbewijsType> _rijbewijsTypes = new();
        public DateTime Geboortedatum { get; set; }
        public string Rijksregisternummer { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Stad { get; set; }
        public string Land { get; set; }
        public Tankkaart Tankkaart { get; set; }
        public Voertuig Voertuig { get; set; }
        public bool IsDeleted { get; set; }

    }
}