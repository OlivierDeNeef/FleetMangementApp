using System;
using System.Collections.Generic;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
   

    public class Voertuig
    {
        public int Id { get; private set; } //verplicht ingevuld
        public string Merk { get; private set; } //verplicht ingevuld
        public string Model { get; private set; } //verplicht ingevuld
        public string Chassisnummer { get; private set; }//verplicht ingevuld
        public WagenType WagenType { get; private set; }//verplicht ingevuld
        public BrandstofType BrandstofType { get; private set; } //verplicht ingevuld
        public string Nummerplaat { get; private set; } //verplicht ingevuld
        public string Kleur { get; private set; }
        public int AantalDeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        public bool IsGearchiveerd { get; private set; }
        public bool IsHybride { get; private set; }


        public Voertuig(int id, string merk, string model, string chassisnummer, string nummerplaat, BrandstofType brandstofType, WagenType wagenType) : this(merk, model, chassisnummer, nummerplaat, brandstofType, wagenType) //Todo: tests schrijven
        {
            ZetId(id);
        }

        public Voertuig(string merk,string model, string chassisnummer, string nummerplaat,BrandstofType brandstofType,WagenType wagenType)
        {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisnummer(chassisnummer);
            ZetNummerplaat(nummerplaat);
            ZetBrandstofType(brandstofType);
            ZetWagenType(wagenType);
        }

     

        /// <summary>
        /// Veranderd id van het voertuig
        /// Controleert of de id positief is, anders geeft deze methode een BestuurderException
        /// </summary>
        /// <param name="id">Id van het voertuig</param>
        public void ZetId(int id)
        {
            if (id < 1) throw new VoertuigException($"ZetId - {nameof(Voertuig)}.{nameof(Id)} kan geen negatieve waarde hebben", new ArgumentException());
            Id = id;
        }
        /// <summary>
        /// check of het merk bestaat in de lijst
        /// </summary>
        /// <param name="merk"></param>
        public void ZetMerk(string merk)
        {
            if (string.IsNullOrWhiteSpace(merk)) throw new VoertuigException($"ZetMerk - {nameof(merk)} kan niet null of leeg zijn");
            Merk = merk.Trim();
        }
        /// <summary>
        /// checking van het automodel, of het in de lijst staat
        /// </summary>
        /// <param name="model">het automodel</param>
        public void ZetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model)) throw new VoertuigException($"ZetModel - {nameof(model)} kan niet null of leeg zijn");
            Model = model.Trim();
        }

        /// <summary>
        /// de check of het chassiesnummer bestaat
        /// </summary>
        /// <param name="chassiesnummer"></param>
        public void ZetChassisnummer(string chassiesnummer)
        {
            if (string.IsNullOrWhiteSpace(chassiesnummer)) throw new VoertuigException("ZetChassiesnummer - Het nummer kan niet null of leeg zijn");
            if (chassiesnummer.Length != 17) throw new VoertuigException($"ZetChassiesnummer - {nameof(chassiesnummer)} heef een exacte lengte van 17 karakters");
            Chassisnummer = chassiesnummer.Trim();
        }

        
        /// <summary>
        /// check of het wagentype bestaat in de lijst
        /// </summary>
        /// <param name="wagenType">personenwagen, bestelwagen etc</param>
        public void ZetWagenType(WagenType wagenType)
        {
            if (wagenType == null) throw new VoertuigException("ZetWagentype - Het wagentype moet ingevuld zijn ");
            WagenType = wagenType;
        }
        /// <summary>
        /// Check of het brandstofTypebestaat
        /// </summary>
        /// <param name="brandstofType">het brandstoftype van het voertuig</param>
        public void ZetBrandstofType(BrandstofType brandstofType)
        {
            BrandstofType = brandstofType ?? throw new VoertuigException("ZetBrandstofType - Het brandstoftype moet ingevuld zijn");
        }

        /// <summary>
        /// Check of het nummerplaat het correcte formaat heeft
        /// </summary>
        /// <param name="nummerplaat">nummerplaat van het voertuig</param>
        public void ZetNummerplaat(string nummerplaat) //Docent besliste dat het enkel via de gewone manier mag zijn
        { 
            if (string.IsNullOrWhiteSpace(nummerplaat))
                throw new VoertuigException("ZetNummerplaat - Nummerplaat moet verplicht ingevuld zijn");
            if (!char.IsDigit(nummerplaat[0]))
                throw new VoertuigException("ZetNummerplaat - Nummerplaat moet beginnen met een cijfer");
            if ((nummerplaat.Length != 7 && nummerplaat.Length != 9)) 
                throw new VoertuigException("ZetNummerplaat - Nummerplaat is niet lang genoeg volgens formaat (1-)ABC-123");
            Nummerplaat = nummerplaat;
        }
        /// <summary>
        /// Trim functie op de kleur van de auto
        /// </summary>
        /// <param name="kleur">kleur van de auto</param>
        public void ZetKleur(string kleur)
        {
            Kleur = kleur.Trim();
        }
        /// <summary>
        /// check aantal deuren van het voertuig dat minstens 3 en max 5 mag zijn
        /// </summary>
        /// <param name="aantalDeuren">aantal deuren van het voertuig</param>
        public void ZetAantalDeuren(int aantalDeuren)
        {
            AantalDeuren = aantalDeuren switch
            {
                < 3 => throw new VoertuigException("ZetAantalDeuren - Voertuig heeft minstens 3 deuren"),
                > 5 => throw new VoertuigException("ZetAantalDeuren - Voertuig mag maximaal 5 deuren hebben"),
                _ => aantalDeuren
            };
        }

        /// <summary>
        /// Zet de bestuurder aan een voertuig
        /// </summary>
        /// <param name="bestuurder">de bestuurder van het voertuig</param>
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if (bestuurder == Bestuurder) 
                throw new VoertuigException("ZetBestuurder - Bestuurder is dezelde als de huidige");
            if (Bestuurder?.Voertuig != null) 
                Bestuurder.VerwijderVoertuig();
            Bestuurder = bestuurder;
            if (bestuurder.Voertuig != this)
            {
                bestuurder.ZetVoertuig(this);
            }
           
        }
        /// <summary>
        /// Verwijderd de bestuurder bij een voertuig, verwijderd een voertuig bij een bestuurder
        /// </summary>
        /// <param name="isGearchiveerd">archiefstatus van de bestuurder of het voertuig</param>
        public void ZetGearchiveerd(bool isGearchiveerd)
        {
            if (isGearchiveerd && Bestuurder != null)
            {
                VerwijderBestuurder();
            }
            IsGearchiveerd = isGearchiveerd;
        }
        /// <summary>
        /// Verwijderd een bestuurder bij een voertuig
        /// </summary>
        public void VerwijderBestuurder()
        {
            if (Bestuurder == null) throw new VoertuigException("VerwijderBestuurder - Bestuurder bestaat niet");
            var bestuurder = Bestuurder;
            Bestuurder = null;
            if(bestuurder.Voertuig != null) bestuurder.VerwijderVoertuig();
        }

        /// <summary>
        /// Controlleer of 2 voertuigen dezelde properties hebben
        /// </summary>
        /// <param name="obj">object om te vergelijken</param>
        /// <returns>True wanneer voertuigen gelijk zijn ander false</returns>
        /*public override bool Equals(object obj) //Todo: tests schrijven
        {
            return obj is Voertuig other && Id == other.Id && Merk == other.Merk && Model == other.Model && Chassisnummer == other.Chassisnummer && Equals(WagenType, other.WagenType) && Equals(BrandstofType, other.BrandstofType) && Nummerplaat == other.Nummerplaat && Kleur == other.Kleur && AantalDeuren == other.AantalDeuren && Equals(Bestuurder, other.Bestuurder) && IsGearchiveerd == other.IsGearchiveerd ;
        }

        /// <summary>
        /// Geef HashCode van voertuig
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Merk);
            hashCode.Add(Model);
            hashCode.Add(Chassisnummer);
            hashCode.Add(WagenType);
            hashCode.Add(BrandstofType);
            hashCode.Add(Nummerplaat);
            hashCode.Add(Kleur);
            hashCode.Add(AantalDeuren);
            hashCode.Add(Bestuurder);
            hashCode.Add(IsGearchiveerd);
            return hashCode.ToHashCode();
        }*/


        public void ZetHybride(bool isHybride)
        {
            IsHybride = isHybride;
        }

        public override bool Equals(object obj)
        {
            return obj is Voertuig voertuig &&
                   Id == voertuig.Id &&
                   Merk == voertuig.Merk &&
                   Model == voertuig.Model &&
                   Chassisnummer == voertuig.Chassisnummer &&
                   EqualityComparer<WagenType>.Default.Equals(WagenType, voertuig.WagenType) &&
                   EqualityComparer<BrandstofType>.Default.Equals(BrandstofType, voertuig.BrandstofType) &&
                   Nummerplaat == voertuig.Nummerplaat &&
                   Kleur == voertuig.Kleur &&
                   AantalDeuren == voertuig.AantalDeuren &&
                   IsGearchiveerd == voertuig.IsGearchiveerd &&
                   IsHybride == voertuig.IsHybride;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Merk);
            hash.Add(Model);
            hash.Add(Chassisnummer);
            hash.Add(WagenType);
            hash.Add(BrandstofType);
            hash.Add(Nummerplaat);
            hash.Add(Kleur);
            hash.Add(AantalDeuren);
            hash.Add(IsGearchiveerd);
            hash.Add(IsHybride);
            return hash.ToHashCode();
        }
    }
}