using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Exceptions.Models;
using DomainLayer.Exceptions.Utilities;
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
        /// Maak een bestuurders object aan en zet zijn properties volgens de methodes en roept de hoofd constructor op 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="naam"></param>
        /// <param name="voornaam"></param>
        /// <param name="geboortedatum"></param>
        /// <param name="rijksregisternummer"></param>
        /// <param name="rijbewijsTypes"></param>
        /// <param name="isGearchiveerd"></param>
        public Bestuurder(int id, string naam, string voornaam, DateTime geboortedatum, string rijksregisternummer, List<RijbewijsType> rijbewijsTypes, bool isGearchiveerd) : this( naam,  voornaam, geboortedatum, rijksregisternummer, rijbewijsTypes, isGearchiveerd) //Todo: test for faulty input
        {
            
            ZetId(id);
            
        }


        /// <summary>
        /// Maak een bestuurders object aan en zet zijn properties volgens de methodes
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="voornaam"></param>
        /// <param name="geboortedatum"></param>
        /// <param name="rijksregisternummer"></param>
        /// <param name="rijbewijsTypes"></param>
        /// <param name="isGearchiveerd"></param>
        public Bestuurder(string naam, string voornaam, DateTime geboortedatum, string rijksregisternummer,List<RijbewijsType> rijbewijsTypes, bool isGearchiveerd = false) //Todo: test for faulty input
        {
            if (rijbewijsTypes.Count < 1) throw new BestuurderException("Bestuurder - bestuurder heeft geen rijbewijs");
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetGearchiveerd(isGearchiveerd);
            _rijbewijsTypes = rijbewijsTypes;
        }



        /// <summary>
        /// Veranderd id van de bestuurder.
        /// Controleert of de id positief is anders geeft deze methode een BestuurderException.
        /// </summary>
        /// <param name="id">Id van de bestuurder.</param>
        public void ZetId(int id)
        {
            if (id < 0) throw new BestuurderException("ZetId - id < 0", new ArgumentOutOfRangeException());
            Id = id;
        }

        /// <summary>
        /// Veranderd achternaam van de bestuurder.
        /// Controleert of de achternaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="naam">De achternaam van de bestuurder.</param>
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new BestuurderException("ZetNaam - Naam is leeg");
            Naam = naam.Trim();
        }

        /// <summary>
        /// Veranderd voornaam van de bestuurder.
        /// Controleert of de voornaam niet leeg is of null is anders geeft deze methode een BestuurderExection.
        /// </summary>
        /// <param name="voornaam">De voornaam van de bestuurder.</param>
        public void ZetVoornaam(string voornaam)
        {
            if (string.IsNullOrWhiteSpace(voornaam)) throw new BestuurderException("ZetVoornaam - Voornaam is leeg");
            Voornaam = voornaam.Trim();
        }

        /// <summary>
        /// Veranderd de geboortedatum van de bestuurder.
        /// Controleert of de bestuurder ouder is dan 18 jaar.
        /// Controleert of de bestuurder jonger is dan 100 jaar.
        /// </summary>
        /// <param name="geboortedatum">De geboortedatum van de bestuurder.</param>
        public void ZetGeboortedatum(DateTime geboortedatum)
        {
            if (DateTime.Today.AddYears(-18) < geboortedatum) throw new BestuurderException("ZetGeboorteDatum - Leeftijd < 18 jaar");
            Geboortedatum = geboortedatum;
        }

        /// <summary>
        /// Veranderd het rijksregisternummer van de bestuurder.
        /// We controleren het rijksregisternummer via de volgende regels --> https://nl.wikipedia.org/wiki/Rijksregisternummer#Samenstelling
        /// </summary>
        /// <param name="rijksregisternummer">Het rijksregisternummer voor de bestuurder</param>
        public void ZetRijksregisternummer(string rijksregisternummer)
        {
            try
            {
                Rijksregisternummer = RijksregisternummerChecker.Parse(rijksregisternummer, Geboortedatum);
            }
            catch (RijksregisternummerCheckerException e)
            {
                
                throw new BestuurderException("Het rijksregisternummer is incorrect");
            }

            
        }

        /// <summary>
        /// Veranderd het adres van de bestuurder
        /// </summary>
        /// <param name="adres">Het adres van de bestuurder</param>
        public void ZetAdres(Adres adres)
        {
            Adres = adres ?? throw new BestuurderException("ZetAdres - Adres is null");
        }


        public void VerwijderAdres()
        {
            if (Adres == null) throw new BestuurderException("VerwijderAdres - Adres is al null");
            Adres = null;
        }

        /// <summary>
        /// Zoekt op of het rijbewijs type voorkomt in de lijst van rijbewijs types van deze bestuurder.
        /// Controleert of het rijbewijs type dat we zoeken niet null is anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat je zoekt.</param>
        /// <returns>True als rijbewijs type voorkomt, False als rijbewijs type niet voor komt.</returns>
        public bool HeeftRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException($"HeeftRijbewijs - rijbewijsType = null");
            return _rijbewijsTypes.Contains(rijbewijsType);
        }

        /// <summary>
        /// Voegt rijbewijs type toe aan de  lijst van rijbewijs types van de bestuurder.
        /// Controleert of het rijbewijs type niet null anders geeft deze een BestuurderException.
        /// Controleert of het rijbewijs type niet al in de lijst staat anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat moet worden toegevoegt.</param>
        public void VoegRijbewijsTypeToe(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException("VoegRijbewijsTypeToe - rijbewijsType = null");
            if (HeeftRijbewijsType(rijbewijsType)) throw new BestuurderException("VoegRijbewijsTypeToe - rijbewijsType bestaat al"); 
            _rijbewijsTypes.Add(rijbewijsType);
        }

        /// <summary>
        /// Verwijderd rijbewijs type uit de  lijst van rijbewijs types van de bestuurder.
        /// Controleert of het rijbewijs type niet null anders geeft deze een BestuurderException.
        /// Controleert of het rijbewijs type al in de lijst staat anders geeft deze een BestuurderException.
        /// </summary>
        /// <param name="rijbewijsType">Het rijbewijs type dat moet worden verwijderd.</param>
        public void VerwijderRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (rijbewijsType == null) throw new BestuurderException("VerwijderRijbewijsType - rijbewijsType = null");
            if (!HeeftRijbewijsType(rijbewijsType)) throw new BestuurderException("VerwijderRijbewijsType - rijbewijsType bestaat niet"); 
            _rijbewijsTypes.Remove(rijbewijsType);
        }

       

        /// <summary>
        /// Veranderd de tankkaart van de bestuurder.
        /// </summary>
        /// <param name="tankkaart">De tankkaart van de bestuurder.</param>
        public void ZetTankkaart(Tankkaart tankkaart)
        {
            if (tankkaart == null) throw new BestuurderException("ZetTankkaart - tankkaart = null");
            if (Tankkaart !=null && Tankkaart.Equals(tankkaart)) throw new BestuurderException("ZetTankkaart - Zelfde tankkaart als huidige");//TODO : extra test
            if (Tankkaart?.Bestuurder != null) Tankkaart.VerwijderBestuurder(); 
            Tankkaart = tankkaart;
            if (tankkaart.Bestuurder == null || !tankkaart.Bestuurder.Equals(this))
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
            if (voertuig == null) throw new BestuurderException("ZetVoertuig - tankkaart = null");
            if (voertuig == Voertuig) throw new BestuurderException("ZetVoertuig - Zelfde tankkaart als huidige");
            if (Voertuig?.Bestuurder != null) Voertuig.VerwijderBestuurder(); 
            Voertuig = voertuig;
            if (voertuig.Bestuurder != this)
            {
                voertuig.ZetBestuurder(this);
            }
           
        }

        /// <summary>
        /// Veranderd de toestand van de bestuurder naar verwijderd of niet verwijderd
        /// </summary>
        /// <param name="isGearchiveerd">De status van verwijderd</param>
        public void ZetGearchiveerd(bool isGearchiveerd)
        {
            if (isGearchiveerd)
            {
                if (Voertuig != null)
                {
                    VerwijderVoertuig();
                }

                if (Tankkaart != null)
                {
                    VerwijderTankkaart();
                }
            }
            
            IsGearchiveerd = isGearchiveerd;
        }

        /// <summary>
        /// Verwijder het voertuig van de bestuurder
        /// </summary>
        public void VerwijderVoertuig()
        {
            if (Voertuig == null) throw new BestuurderException("VerwijderVoetuig - Voertuig is al null");
            var voertuig = Voertuig;
            Voertuig = null;
            if (voertuig.Bestuurder != null) voertuig.VerwijderBestuurder();
        }

        /// <summary>
        /// Verwijderd de tankkaart van de bestuurder
        /// </summary>
        public void VerwijderTankkaart()
        {
            if (Tankkaart == null) throw new BestuurderException("VerwijderTankkaart - Tankkaart is al null");
            var tankkaart = Tankkaart;
            Tankkaart = null;
            if (tankkaart.Bestuurder != null) tankkaart.VerwijderBestuurder();
        }


        /// <summary>
        /// Geeft de rijbewijstypes van de bestuurder
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<RijbewijsType> GeefRijbewijsTypes() //Todo: tests schrijven
        {
            return _rijbewijsTypes;
        }

        public override bool Equals(object obj)
        {
            return obj is Bestuurder bestuurder &&
                   Id == bestuurder.Id &&
                   Naam == bestuurder.Naam &&
                   Voornaam == bestuurder.Voornaam &&
                   EqualityComparer<List<RijbewijsType>>.Default.Equals(_rijbewijsTypes, bestuurder._rijbewijsTypes) &&
                   Geboortedatum == bestuurder.Geboortedatum &&
                   Rijksregisternummer == bestuurder.Rijksregisternummer &&
                   IsGearchiveerd == bestuurder.IsGearchiveerd;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Naam, Voornaam, _rijbewijsTypes, Geboortedatum, Rijksregisternummer, IsGearchiveerd);
        }

        /// <summary>
        /// Controlleert of 2 bestuurder hetzelfde zijn
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /*public override bool Equals(object obj) //Todo: tests schrijven
        {
            return obj is Bestuurder other &&
                   _rijbewijsTypes.All(other._rijbewijsTypes.Contains) &&
                   _rijbewijsTypes.Count == other._rijbewijsTypes.Count &&
                   Id == other.Id &&
                   Naam == other.Naam &&
                   Voornaam == other.Voornaam &&
                   Geboortedatum.Equals(other.Geboortedatum) &&
                   Rijksregisternummer == other.Rijksregisternummer &&
                   IsGearchiveerd == other.IsGearchiveerd;
        }

        /// <summary>
        /// Geeft de hash code van een bestuurder
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() 
        {
            var hashCode = new HashCode();
            hashCode.Add(_rijbewijsTypes);
            hashCode.Add(Id);
            hashCode.Add(Naam);
            hashCode.Add(Voornaam);
            hashCode.Add(Geboortedatum);
            hashCode.Add(Rijksregisternummer);
            hashCode.Add(Adres);
            hashCode.Add(Tankkaart);
            hashCode.Add(Voertuig);
            hashCode.Add(IsGearchiveerd);
            return hashCode.ToHashCode();
        }*/
    }
}