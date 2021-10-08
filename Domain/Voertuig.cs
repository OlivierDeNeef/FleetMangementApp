using System;
using System.Buffers;
using DomainLayer.Exceptions;

namespace DomainLayer
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
        public bool IsDeleted { get; private set; }


        /// <summary>
        /// Veranderd id van het voertuig
        /// Controleert of de id positief is, anders geeft deze methode een BestuurderException
        /// </summary>
        /// <param name="id">Id van het voertuig</param>
        public void SetId(int id)
        {
            if (id < 0)
                throw new VoertuigExceptions($"{nameof(Voertuig)}.{nameof(Id)} kan geen negatieve waarde hebben", new ArgumentException());
            this.Id = id;
        }
        /// <summary>
        /// check of het merk bestaat in de lijst
        /// </summary>
        /// <param name="merk"></param>
        public void SetMerk(string merk)
        {
            if (string.IsNullOrEmpty(merk.Trim()))
                throw new VoertuigExceptions($"{nameof(merk)} kan niet null of leeg zijn");
            this.Merk = merk.Trim();
        }
        /// <summary>
        /// checking van het automodel, of het in de lijst staat
        /// </summary>
        /// <param name="model">het automodel</param>
        public void SetModel(string model)
        {
            if (string.IsNullOrEmpty(model.Trim()))
                throw new VoertuigExceptions($"{nameof(model)} kan niet null of leeg zijn");
            this.Model = model.Trim();
        }

        /// <summary>
        /// de check of het chassiesnummer bestaat
        /// </summary>
        /// <param name="chassiesnummer"></param>
        public void SetChassisnummer(string chassiesnummer)
        {
            if (string.IsNullOrEmpty(chassiesnummer.Trim()))
                throw new VoertuigExceptions("Het nummer kan niet null of leeg zijn");
            if (chassiesnummer.Length != 17)
                throw new VoertuigExceptions($"{nameof(chassiesnummer)} heef een exacte lengte van 17 karakters");
            
            this.Chassisnummer = chassiesnummer.Trim();
        }

        
        /// <summary>
        /// check of het wagentype bestaat in de lijst
        /// </summary>
        /// <param name="wagenType">personenwagen, bestelwagen etc</param>
        public void SetWagenType(WagenType wagenType)
        {
            if (wagenType == null)
            {
                throw new VoertuigExceptions($"Het wagentype moet ingevuld zijn ");
            }

            if (wagenType.Id <= 0)
            {
                throw new VoertuigExceptions($"{nameof(wagenType.Type)} heeft een ongeldig id");
            }

            if (wagenType.Type.Trim().Length == 0)
            {
                throw new VoertuigExceptions($"Ongeldige waarde van het wagentype");
            }

            WagenType = wagenType;
            
        }

        public void SetBrandstofType(BrandstofType brandstofType)
        {
            if (brandstofType == null)
            {
                throw new VoertuigExceptions("Het brandstoftype moet ingevuld zijn");
            }

            BrandstofType = brandstofType;
        }

        public void SetNummerplaat(string nummerplaat)
        {
            if (string.IsNullOrEmpty(nummerplaat))
                throw new VoertuigExceptions("Nummerplaat moet verplicht ingevuld zijn");
            if (nummerplaat.Length != 7 && nummerplaat.Length != 9)
                throw new VoertuigExceptions("Nummerplaat is niet lang genoeg volgens formaat (1-)ABC-123");
            
            Nummerplaat = nummerplaat;
        }
        
        public void SetKleur(string kleur)
        {
            Kleur = kleur.Trim();
        }

        public void SetAantalDeuren(int aantalDeuren)
        {
            if (aantalDeuren < 3)
            {
                throw new VoertuigExceptions("Voertuig heeft minstens 3 deuren");
            }

            if (aantalDeuren > 5)
                throw new VoertuigExceptions("Voertuig mag maximaal 5 deuren hebben");
            AantalDeuren = aantalDeuren;
        }

        public void SetBestuurder(Bestuurder bestuurder)
        {
            //Todo: afstellen op bestuurder
            Bestuurder = bestuurder;
        }

        public void SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}