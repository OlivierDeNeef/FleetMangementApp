using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using DomainLayer.Exceptions;

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
        
        /// <summary>
        /// Veranderd id van de bestuurder.
        /// Controleert of de id positief is anders geeft deze methode een BestuurderException.
        /// </summary>
        /// <param name="id">Id van de bestuurder</param>
        public void SetId(int id)
        {
            if (id < 0) throw  new BestuurderException( $"{nameof(Bestuurder)}.{nameof(Id)} kan geen negatieve waarde bevatten", new ArgumentOutOfRangeException());
            this.Id = id;
        }

        /// <summary>
        /// Veranderd naam van de bestuurder.
        /// Controleert of de naam niet leef is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="naam">De Achternaam van de bestuurder</param>
        public void SetNaam(string naam)
        {
            if(string.IsNullOrEmpty(naam.Trim()))throw  new BestuurderException($"{nameof(Bestuurder)}.{nameof(naam)} Kan niet null of leeg zijn");
            this.Naam = naam;
        }

    }
}