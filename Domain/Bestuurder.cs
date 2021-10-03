using System;
using System.Collections.Generic;
using System.Linq;
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
        public Tankkaart Tankkaart { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public bool IsDeleted { get; private set; }
        public string Postcode { get; private set; }

        /// <summary>
        /// Veranderd id van de bestuurder.
        /// Controleert of de id positief is anders geeft deze methode een BestuurderException.
        /// </summary>
        /// <param name="id">Id van de bestuurder.</param>
        public void SetId(int id)
        {
            if (id < 0) throw new BestuurderException($"{nameof(Bestuurder)}.{nameof(Id)} kan geen negatieve waarde bevatten", new ArgumentOutOfRangeException());
            this.Id = id;
        }

        /// <summary>
        /// Veranderd achternaam van de bestuurder.
        /// Controleert of de achternaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="naam">De achternaam van de bestuurder.</param>
        public void SetNaam(string naam)
        {
            if (string.IsNullOrEmpty(naam.Trim())) throw new BestuurderException($"{nameof(Bestuurder)}.{nameof(naam)} Kan niet null of leeg zijn");
            this.Naam = naam.Trim();
        }

        /// <summary>
        /// Veranderd voornaam van de bestuurder.
        /// Controleert of de voornaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="voornaam">De voornaam van de bestuurder.</param>
        public void SetVoornaam(string voornaam)
        {
            if (string.IsNullOrEmpty(voornaam.Trim())) throw new BestuurderException($"{nameof(Bestuurder)}.{nameof(voornaam)} Kan niet null of leeg zijn");
            this.Voornaam = voornaam.Trim();
        }

        /// <summary>
        /// Veranderd de geboortedatum van de bestuurder.
        /// Controleert of de bestuurder ouder is dan 18 jaar.
        /// Controleert of de bestuurder jonger is dan 100 jaar.
        /// </summary>
        /// <param name="geboortedatum">De geboortedatum van de bestuurder.</param>
        public void SetGeboortedatum(DateTime geboortedatum)
        {
            if (DateTime.Today.AddYears(-18) < geboortedatum) throw new BestuurderException("Niet oud genoeg om bestuurder te zijn");
            if (DateTime.Today.AddYears(-100) > geboortedatum) throw new BestuurderException("Onjuiste geboortedatum");
            this.Geboortedatum = geboortedatum;
        }

        /// <summary>
        /// Veranderd het rijksregisternummer van de bestuurder.
        /// We controleren het rijksregisternummer via de volgende regels --> https://nl.wikipedia.org/wiki/Rijksregisternummer#Samenstelling
        /// </summary>
        /// <param name="rijksregisternummer">Het rijksregisternummer voor de bestuurder</param>
        public void SetRijksregisternummer(string rijksregisternummer)
        {
            if (string.IsNullOrEmpty(rijksregisternummer.Trim())) throw new BestuurderException($"{nameof(Rijksregisternummer)} kan niet leeg of null zijn.");
            var cleanRijksregisternummer = rijksregisternummer.Trim().Replace(".", "").Replace("-", "").Replace(" ", "");
            if (cleanRijksregisternummer.Length != 11) throw new BestuurderException($"Het {nameof(Rijksregisternummer)} moet 11 karakters hebben");
            if (!cleanRijksregisternummer.All(char.IsDigit)) throw new BestuurderException($"Het {nameof(rijksregisternummer)} kan alleen maar cijfer bevatten");
            if (Geboortedatum.ToString("yyMMdd") != cleanRijksregisternummer.Substring(0, 6)) throw new BestuurderException($"Het {Rijksregisternummer} komt niet overeen met de geboortedatum");

            var tweedeDeel = int.Parse(cleanRijksregisternummer.Substring(6, 3));
            if (1 > tweedeDeel || tweedeDeel > 998) throw new BestuurderException($"Het {nameof(rijksregisternummer)} heeft niet het juist formaat");


            var aaneengeschakeldGetal = Geboortedatum.Year > 1999 ? int.Parse("2" + cleanRijksregisternummer.Substring(0, 9)) : int.Parse(cleanRijksregisternummer.Substring(0, 9));

            var controlGetal = 97 - (aaneengeschakeldGetal % 97);
            if (controlGetal.ToString() != cleanRijksregisternummer.Substring(9, 2)) throw new BestuurderException($"Het {nameof(Rijksregisternummer)} is ongeldig het controle getal klopt niet");
            this.Rijksregisternummer = cleanRijksregisternummer;


        }

        /// <summary>
        /// Zoekt op of het rijbewijs type voorkomt in de lijst van rijbewijs types van deze bestuurder.
        /// Controleert of het rijbewijs type dat we zoeken niet null is anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat je zoekt.</param>
        /// <returns>True als rijbewijs type voorkomt, False als rijbewijs type niet voor komt.</returns>
        public bool HasRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException($"Zoeken op {nameof(RijbewijsType)} gaat niet wanneer deze null is.");
            return this._rijbewijsTypes.Contains(rijbewijsType);
        }

        /// <summary>
        /// Voegt rijbewijs type toe aan de  lijst van rijbewijs types van de bestuurder.
        /// Controleert of het rijbewijs type niet null anders geeft deze een BestuurderException.
        /// Controleert of het rijbewijs type niet al in de lijst staat anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat moet worden toegevoegt.</param>
        public void AddRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException($"Een {nameof(RijbewijsType)} toevoegen gaat niet wanneer deze null is.");
            if (HasRijbewijsType(rijbewijsType)) throw new BestuurderException($"Het {nameof(RijbewijsType)} bevindt zich al in de lijst"); //Ask: Exception of kan men gewoon verder
            this._rijbewijsTypes.Add(rijbewijsType);
        }

        /// <summary>
        /// Verwijderd rijbewijs type uit de  lijst van rijbewijs types van de bestuurder.
        /// Controleert of het rijbewijs type niet null anders geeft deze een BestuurderException.
        /// Controleert of het rijbewijs type al in de lijst staat anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat moet worden verwijderd.</param>
        public void RemoveRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException($"Een {nameof(RijbewijsType)} verwijderen gaat niet wanneer deze null is.");
            if (!HasRijbewijsType(rijbewijsType)) throw new BestuurderException($"Het {nameof(RijbewijsType)} bevindt zich niet in de lijst"); //Ask: Exception of kan men gewoon verder
            this._rijbewijsTypes.Remove(rijbewijsType);
        }

        /// <summary>
        /// Veranderd de straat van de bestuurder.
        /// </summary>
        /// <param name="straat">De straat van de bestuurder.</param>
        public void SetStraat(string straat)
        {
            this.Straat = straat?.Trim();
        }

        /// <summary>
        /// Veranderd de huisnummer van de bestuurder.
        /// </summary>
        /// <param name="huisnummer">Huisnummer van de bestuurder.</param>
        public void SetHuisnummer(string huisnummer)
        {
            this.Huisnummer = huisnummer?.Trim();
        }

        /// <summary>
        /// Veranderd de stad van de bestuurder.
        /// </summary>
        /// <param name="stad">De stad van de bestuurder.</param>
        public void SetStad(string stad)
        {
            this.Stad = stad?.Trim();
        }

        /// <summary>
        /// Veranderd het land van de bestuurder.
        /// </summary>
        /// <param name="land">Het land van de bestuurder.</param>
        public void SetLand(string land)
        {
            this.Land = land?.Trim();
        }

        /// <summary>
        /// Veranderd de tankkaart van de bestuurder.
        /// </summary>
        /// <param name="tankkaart">De tankkaart van de bestuurder.</param>
        public void SetTankkaart(Tankkaart tankkaart)
        {
            this.Tankkaart = tankkaart;
        }

        /// <summary>
        /// Veranderd het voertuig van de bestuurder.
        /// </summary>
        /// <param name="voertuig">Het voertuig van de bestuurder</param>
        public void SetVoertuig(Voertuig voertuig)
        {
            this.Voertuig = voertuig;
        }

        /// <summary>
        /// Veranderd de toestand van de bestuurder naar verwijderd of niet verwijderd
        /// </summary>
        /// <param name="isDeleted">De status van verwijderd</param>
        public void SetDeleted(bool isDeleted)
        {
            this.IsDeleted = isDeleted;
        }

        /// <summary>
        /// Veranderd de toestand van de bestuurders postcode
        /// </summary>
        /// <param name="postcode">De postcode van de bestuurder</param>
        public void SetPostcode(string postcode)
        {
            this.Postcode = postcode?.Trim();
        }
    }
}