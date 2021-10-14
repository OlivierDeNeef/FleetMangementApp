using System;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    /// <summary>
    /// Todo : Zet methodenamen Zoals in class diagram 
    /// </summary>


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


        /// <summary>
        /// Veranderd id van het voertuig
        /// Controleert of de id positief is, anders geeft deze methode een BestuurderException
        /// </summary>
        /// <param name="id">Id van het voertuig</param>
        public void ZetId(int id)
        {
            if (id < 0) throw new VoertuigException($"{nameof(Voertuig)}.{nameof(Id)} kan geen negatieve waarde hebben", new ArgumentException());
            this.Id = id;
        }
        /// <summary>
        /// check of het merk bestaat in de lijst
        /// </summary>
        /// <param name="merk"></param>
        public void ZetMerk(string merk)
        {
            if (string.IsNullOrWhiteSpace(merk)) throw new VoertuigException($"{nameof(merk)} kan niet null of leeg zijn");
            this.Merk = merk.Trim();
        }
        /// <summary>
        /// checking van het automodel, of het in de lijst staat
        /// </summary>
        /// <param name="model">het automodel</param>
        public void ZetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model)) throw new VoertuigException($"{nameof(model)} kan niet null of leeg zijn");
            this.Model = model.Trim();
        }

        /// <summary>
        /// de check of het chassiesnummer bestaat
        /// </summary>
        /// <param name="chassiesnummer"></param>
        public void ZetChassisnummer(string chassiesnummer)
        {
            if (string.IsNullOrWhiteSpace(chassiesnummer)) throw new VoertuigException("Het nummer kan niet null of leeg zijn");
            if (chassiesnummer.Length != 17) throw new VoertuigException($"{nameof(chassiesnummer)} heef een exacte lengte van 17 karakters");
            this.Chassisnummer = chassiesnummer.Trim();
        }

        
        /// <summary>
        /// check of het wagentype bestaat in de lijst
        /// </summary>
        /// <param name="wagenType">personenwagen, bestelwagen etc</param>
        public void ZetWagenType(WagenType wagenType)
        {
            if (wagenType == null) throw new VoertuigException($"Het wagentype moet ingevuld zijn ");
            if (wagenType.Id <= 0) throw new VoertuigException($"{nameof(wagenType.Type)} heeft een ongeldig id");
            if (wagenType.Type.Trim().Length == 0) throw new VoertuigException($"Ongeldige waarde van het wagentype");
            WagenType = wagenType;
        }

        public void ZetBrandstofType(BrandstofType brandstofType)
        {
            BrandstofType = brandstofType ?? throw new VoertuigException("Het brandstoftype moet ingevuld zijn");
        }

        public void ZetNummerplaat(string nummerplaat)
        {
            if (string.IsNullOrEmpty(nummerplaat)) throw new VoertuigException("Nummerplaat moet verplicht ingevuld zijn");
            if ((nummerplaat.Length != 7 && nummerplaat.Length != 9)) throw new VoertuigException("Nummerplaat is niet lang genoeg volgens formaat (1-)ABC-123");
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
                < 3 => throw new VoertuigException("Voertuig heeft minstens 3 deuren"),
                > 5 => throw new VoertuigException("Voertuig mag maximaal 5 deuren hebben"),
                _ => aantalDeuren
            };
        }

        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if (bestuurder == Bestuurder) throw new VoertuigException("Bestuurder is dezelde als de huidige");
            if (Bestuurder?.Voertuig != null) Bestuurder.VerwijderVoertuig();
            Bestuurder = bestuurder;
            if (bestuurder.Voertuig != this)
            {
                bestuurder.ZetVoertuig(this);
            }
           
        }

        public void ZetGearchiveerd(bool isGearchiveerd)
        {
            IsGearchiveerd = isGearchiveerd;
        }

        public void VerwijderBestuurder()
        {
            if (Bestuurder == null) throw new VoertuigException();
            Bestuurder = null;

        }
    }
}