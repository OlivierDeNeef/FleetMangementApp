using System;
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


        public Voertuig(int id, string merk, string model, string chassisnummer, string nummerplaat, BrandstofType brandstofType, WagenType wagenType) : this(merk, model, chassisnummer, nummerplaat, brandstofType, wagenType)
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

        public Voertuig()
        {

        }

        /// <summary>
        /// Veranderd id van het voertuig
        /// Controleert of de id positief is, anders geeft deze methode een BestuurderException
        /// </summary>
        /// <param name="id">Id van het voertuig</param>
        public void ZetId(int id)
        {
            if (id < 0) throw new VoertuigException($"ZetId - {nameof(Voertuig)}.{nameof(Id)} kan geen negatieve waarde hebben", new ArgumentException());
            this.Id = id;
        }
        /// <summary>
        /// check of het merk bestaat in de lijst
        /// </summary>
        /// <param name="merk"></param>
        public void ZetMerk(string merk)
        {
            if (string.IsNullOrWhiteSpace(merk)) throw new VoertuigException($"ZetMerk - {nameof(merk)} kan niet null of leeg zijn");
            this.Merk = merk.Trim();
        }
        /// <summary>
        /// checking van het automodel, of het in de lijst staat
        /// </summary>
        /// <param name="model">het automodel</param>
        public void ZetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model)) throw new VoertuigException($"ZetModel - {nameof(model)} kan niet null of leeg zijn");
            this.Model = model.Trim();
        }

        /// <summary>
        /// de check of het chassiesnummer bestaat
        /// </summary>
        /// <param name="chassiesnummer"></param>
        public void ZetChassisnummer(string chassiesnummer)
        {
            if (string.IsNullOrWhiteSpace(chassiesnummer)) throw new VoertuigException("ZetChassiesnummer - Het nummer kan niet null of leeg zijn");
            if (chassiesnummer.Length != 17) throw new VoertuigException($"ZetChassiesnummer - {nameof(chassiesnummer)} heef een exacte lengte van 17 karakters");
            this.Chassisnummer = chassiesnummer.Trim();
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

        public void ZetBrandstofType(BrandstofType brandstofType)
        {
            BrandstofType = brandstofType ?? throw new VoertuigException("ZetBrandstofType - Het brandstoftype moet ingevuld zijn");
        }

        public void ZetNummerplaat(string nummerplaat) //Docent besliste dat het enkel via de gewone manier mag zijn
        {
            if (!char.IsDigit(nummerplaat[0]))
                throw new VoertuigException("ZetNummerplaat - Nummerplaat moet beginnen met een cijfer");
            if (string.IsNullOrWhiteSpace(nummerplaat))
                throw new VoertuigException("ZetNummerplaat - Nummerplaat moet verplicht ingevuld zijn");
            if ((nummerplaat.Length != 7 && nummerplaat.Length != 9)) 
                throw new VoertuigException("ZetNummerplaat - Nummerplaat is niet lang genoeg volgens formaat (1-)ABC-123");
            Nummerplaat = nummerplaat;
        }
        
        public void ZetKleur(string kleur)
        {
            Kleur = kleur.Trim();
        }

        public void ZetAantalDeuren(int aantalDeuren)
        {
            AantalDeuren = aantalDeuren switch
            {
                < 3 => throw new VoertuigException("ZetAantalDeuren - Voertuig heeft minstens 3 deuren"),
                > 5 => throw new VoertuigException("ZetAantalDeuren - Voertuig mag maximaal 5 deuren hebben"),
                _ => aantalDeuren
            };
        }

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

        public void ZetGearchiveerd(bool isGearchiveerd)
        {
            if (isGearchiveerd)
            {
                Bestuurder.VerwijderVoertuig();
                VerwijderBestuurder();
            }
            IsGearchiveerd = isGearchiveerd;
        }

        public void VerwijderBestuurder()
        {
            if (Bestuurder == null) throw new VoertuigException("VerwijderBestuurder - Bestuurder bestaat niet");
            Bestuurder = null;

        }
    }
}