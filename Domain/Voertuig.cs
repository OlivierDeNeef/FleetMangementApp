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
        /// 
        /// </summary>
        /// <param name="merk"></param>
        public void SetMerk(string merk)
        {
            if (string.IsNullOrEmpty(merk.Trim()))
                throw new VoertuigExceptions($"{nameof(merk)} kan niet null of leeg zijn");
            this.Merk = merk.Trim();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(string model)
        {
            if (string.IsNullOrEmpty(model.Trim()))
                throw new VoertuigExceptions($"{nameof(model)} kan niet null of leeg zijn");
            this.Model = model.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chassiesnummer"></param>
        public void SetChassisnummer(string chassiesnummer)
        {
            if (string.IsNullOrEmpty(chassiesnummer.Trim()))
                throw new VoertuigExceptions($"{nameof(chassiesnummer)} bestaat kan niet null of leeg zijn");
            

            this.Chassisnummer = chassiesnummer.Trim();
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wagenType"></param>
        public void SetWagenType(WagenType wagenType)
        {
            if (wagenType == null)
            {

            }
        }
      
    }
}