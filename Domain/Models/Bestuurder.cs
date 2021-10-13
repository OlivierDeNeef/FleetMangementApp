using System;
using System.Collections.Generic;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Models;
using DomainLayer.Utilities;

namespace DomainLayer.Models
{
    public class Bestuurder
    {
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }
        private readonly List<RijbewijsType> _rijbewijsTypes = new();
        public DateTime Geboortedatum { get; private set; }
        public string Rijksregisternummer { get; private set; }
        public Adres Adres { get; private set; }
        public Tankkaart Tankkaart { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public bool IsGearchiveerd { get; private set; }
        

        /// <summary>
        /// Veranderd id van de bestuurder.
        /// Controleert of de id positief is anders geeft deze methode een BestuurderException.
        /// </summary>
        /// <param name="id">Id van de bestuurder.</param>
        public void ZetId(int id)
        {
            if (id < 0) throw new BestuurderException($"{nameof(Bestuurder)}.{nameof(Id)} kan geen negatieve waarde bevatten", new ArgumentOutOfRangeException());
            this.Id = id;
        }

        /// <summary>
        /// Veranderd achternaam van de bestuurder.
        /// Controleert of de achternaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="naam">De achternaam van de bestuurder.</param>
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new BestuurderException($"{nameof(Bestuurder)}.{nameof(naam)} Kan niet null of leeg zijn");
            this.Naam = naam.Trim();
        }

        /// <summary>
        /// Veranderd voornaam van de bestuurder.
        /// Controleert of de voornaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="voornaam">De voornaam van de bestuurder.</param>
        public void ZetVoornaam(string voornaam)
        {
            if (string.IsNullOrWhiteSpace(voornaam)) throw new BestuurderException($"{nameof(Bestuurder)}.{nameof(voornaam)} Kan niet null of leeg zijn");
            this.Voornaam = voornaam.Trim();
        }

        /// <summary>
        /// Veranderd de geboortedatum van de bestuurder.
        /// Controleert of de bestuurder ouder is dan 18 jaar.
        /// Controleert of de bestuurder jonger is dan 100 jaar.
        /// </summary>
        /// <param name="geboortedatum">De geboortedatum van de bestuurder.</param>
        public void ZetGeboortedatum(DateTime geboortedatum)
        {
            if (DateTime.Today.AddYears(-18) < geboortedatum) throw new BestuurderException("Niet oud genoeg om bestuurder te zijn");
            this.Geboortedatum = geboortedatum;
        }

        /// <summary>
        /// Veranderd het rijksregisternummer van de bestuurder.
        /// We controleren het rijksregisternummer via de volgende regels --> https://nl.wikipedia.org/wiki/Rijksregisternummer#Samenstelling
        /// </summary>
        /// <param name="rijksregisternummer">Het rijksregisternummer voor de bestuurder</param>
        public void ZetRijksregisternummer(string rijksregisternummer)
        {
            this.Rijksregisternummer = RijksregisternummerChecker.Parse(rijksregisternummer, Geboortedatum);
        }

        /// <summary>
        /// Veranderd het adres van de bestuurder
        /// </summary>
        /// <param name="adres">Het adres van de bestuurder</param>
        public void ZetAdres(Adres adres)
        {
            this.Adres = adres;
        }

        /// <summary>
        /// Zoekt op of het rijbewijs type voorkomt in de lijst van rijbewijs types van deze bestuurder.
        /// Controleert of het rijbewijs type dat we zoeken niet null is anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat je zoekt.</param>
        /// <returns>True als rijbewijs type voorkomt, False als rijbewijs type niet voor komt.</returns>
        public bool HeeftRijbewijsType(RijbewijsType rijbewijsType)
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
        public void VoegRijbewijsTypeToe(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException($"Een {nameof(RijbewijsType)} toevoegen gaat niet wanneer deze null is.");
            if (HeeftRijbewijsType(rijbewijsType)) throw new BestuurderException($"Het {nameof(RijbewijsType)} bevindt zich al in de lijst"); 
            this._rijbewijsTypes.Add(rijbewijsType);
        }

        /// <summary>
        /// Verwijderd rijbewijs type uit de  lijst van rijbewijs types van de bestuurder.
        /// Controleert of het rijbewijs type niet null anders geeft deze een BestuurderException.
        /// Controleert of het rijbewijs type al in de lijst staat anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat moet worden verwijderd.</param>
        public void VerwijderRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException($"Een {nameof(RijbewijsType)} verwijderen gaat niet wanneer deze null is.");
            if (!HeeftRijbewijsType(rijbewijsType)) throw new BestuurderException($"Het {nameof(RijbewijsType)} bevindt zich niet in de lijst"); 
            this._rijbewijsTypes.Remove(rijbewijsType);
        }

       

        /// <summary>
        /// Veranderd de tankkaart van de bestuurder.
        /// </summary>
        /// <param name="tankkaart">De tankkaart van de bestuurder.</param>
        public void ZetTankkaart(Tankkaart tankkaart)
        {
            if (tankkaart == Tankkaart) throw new BestuurderException();//TODO : extra test
            if (Tankkaart?.Bestuurder != null) Tankkaart.VerwijderBestuurder(); 
            this.Tankkaart = tankkaart;
            if (tankkaart.Bestuurder != this)
            {
                tankkaart.ZetBestuurder(this);
            }
          
        }

        /// <summary>
        /// Veranderd het voertuig van de bestuurder.
        /// </summary>
        /// <param name="voertuig">Het voertuig van de bestuurder</param>
        public void ZetVoertuig(Voertuig voertuig)
        {
            if (voertuig == null) throw new BestuurderException();
            if (voertuig == Voertuig) throw new BestuurderException();
            if (Voertuig?.Bestuurder != null) Voertuig.VerwijderBestuurder(); //Todo : Extra test
            this.Voertuig = voertuig;
            if (voertuig.Bestuurder != this)
            {
                voertuig.SetBestuurder(this);
            }
           
        }

        /// <summary>
        /// Veranderd de toestand van de bestuurder naar verwijderd of niet verwijderd
        /// </summary>
        /// <param name="isGearchiveerd">De status van verwijderd</param>
        public void ZetGearchiveerd(bool isGearchiveerd)
        {
            this.IsGearchiveerd = isGearchiveerd;
        }

        /// <summary>
        /// Verwijder het voertuig van de bestuurder
        /// </summary>
        public void VerwijderVoertuig()
        {
            if (Voertuig == null) throw new BestuurderException(); //Todo : extra test
            Voertuig = null;
        }

        /// <summary>
        /// Verwijderd de tankkaart van de bestuurder
        /// </summary>
        public void VerwijderTankkaart()
        {
            if (Tankkaart == null) throw new BestuurderException(); //Todo: extra test
            Tankkaart = null;
        }
    }
}