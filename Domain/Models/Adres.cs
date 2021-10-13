using System;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Models;

namespace DomainLayer
{
    public class Adres
    {
        public int Id { get; set; }
        public string Straat { get; private set; }
        public string Huisnummer { get; private set; }
        public string Stad { get; private set; } 
        public string Postcode { get; private set; }
        public string Land { get; private set; }


        /// <summary>
        /// Veranderd id van het adres.
        /// Controleert of de id positief is anders geeft deze methode een AdresException.
        /// </summary>
        /// <param name="id">Id van het adres.</param>
        public void ZetId(int id)
        {
            if (id < 0) throw new AdresException($"{nameof(Adres)}.{nameof(Id)} kan geen negatieve waarde bevatten", new ArgumentOutOfRangeException());
            this.Id = id;
        }

        /// <summary>
        /// Veranderd de straat van het adres.
        /// </summary>
        /// <param name="straat">De straat van het adres.</param>
        public void ZetStraat(string straat)
        {
            this.Straat = straat?.Trim();
        }

        /// <summary>
        /// Veranderd de huisnummer van het adres.
        /// </summary>
        /// <param name="huisnummer">Huisnummer van het adres.</param>
        public void ZetHuisnummer(string huisnummer)
        {
            this.Huisnummer = huisnummer?.Trim();
        }

        /// <summary>
        /// Veranderd de stad van het adres.
        /// </summary>
        /// <param name="stad">De stad van het adres.</param>
        public void ZetStad(string stad)
        {
            this.Stad = stad?.Trim();
        }

        /// <summary>
        /// Veranderd de postocode van het adres
        /// </summary>
        /// <param name="postcode">De postcode van de bestuurder</param>
        public void ZetPostcode(string postcode)
        {
            this.Postcode = postcode?.Trim();
        }

        /// <summary>
        /// Veranderd het land van de bestuurder.
        /// </summary>
        /// <param name="land">Het land van de bestuurder.</param>
        public void ZetLand(string land)
        {
            this.Land = land?.Trim();
        }

    }
}