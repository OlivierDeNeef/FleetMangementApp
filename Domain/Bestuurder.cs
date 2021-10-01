using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using DomainLayer.Exceptions;

namespace DomainLayer
{
    public class Bestuurder
    {
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }
        private readonly List<RijbewijsType> _rijbewijsTypes = new();
        public DateTime Geboortedatum { get; private set; }
        public string Rijksregisternummer { get; private set; }
        public string Straat { get; private set; }
        public string Huisnummer { get; private set; }
        public string Stad { get; private set; }
        public string Land { get; private set; }
        public Tankkaart Tankkaart { get;private set; }
        public Voertuig Voertuig { get; private set; }
        public bool IsDeleted { get; private set; }
        
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
        /// Veranderd achternaam van de bestuurder.
        /// Controleert of de achternaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="naam">De achternaam van de bestuurder</param>
        public void SetNaam(string naam)
        {
            if(string.IsNullOrEmpty(naam.Trim()))throw  new BestuurderException($"{nameof(Bestuurder)}.{nameof(naam)} Kan niet null of leeg zijn");
            this.Naam = naam;
        }

        /// <summary>
        /// Veranderd voornaam van de bestuurder.
        /// Controleert of de voornaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="voornaam">De voornaam van de bestuurder</param>
        public void SetVoornaam(string voornaam)
        {
            if (string.IsNullOrEmpty(voornaam.Trim())) throw new BestuurderException($"{nameof(Bestuurder)}.{nameof(voornaam)} Kan niet null of leeg zijn");
            this.Voornaam = voornaam;
        }

        /// <summary>
        /// Zoekt op of het rijbewijs type voorkomt in de lijst van rijbewijs types van deze bestuurder.
        /// Controleert of het rijbewijs type dat we zoeken niet null is anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat je zoekt.</param>
        /// <returns>True als rijbewijs type voorkomt, False als rijbewijs type niet voor komt.</returns>
        public bool HasRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException($"Zoeken op {nameof(RijbewijsType)} gaat niet wanneer de null is.");
            return _rijbewijsTypes.Contains(rijbewijsType);
        }

    }
}