using System;
using System.Text;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class Adres
    {
        public string Straat { get; private set; }
        public string Huisnummer { get; private set; }
        public string Busnummer { get; set; }   
        public string Stad { get; private set; } 
        public string Postcode { get; private set; }
        public string Land { get; private set; }

        public Adres(string straat, string huisnummer, string stad, string postcode, string land)//Todo: tests schrijven
        {
           ZetStraat(straat);
           ZetHuisnummer(huisnummer);
           ZetStad(stad);
           ZetPostcode(postcode);
           ZetLand(land);
        }

        

       /// <summary>
        /// Veranderd de straat van het adres.
        /// </summary>
        /// <param name="straat">De straat van het adres.</param>
        public void ZetStraat(string straat)
        {
            if (string.IsNullOrWhiteSpace(straat)) throw new AdresException("ZetStraat - straat is null of leeg");
            Straat = straat.Trim();
        }

        /// <summary>
        /// Veranderd het busnummer van het adres
        /// </summary>
        /// <param name="busnummer">Busnummer van het adres</param>
        public void ZetBusnummer(string busnummer)
        {
            Busnummer = busnummer;
        }


        /// <summary>
        /// Veranderd de huisnummer van het adres.
        /// </summary>
        /// <param name="huisnummer">Huisnummer van het adres.</param>
        public void ZetHuisnummer(string huisnummer)
        {
            if (string.IsNullOrWhiteSpace(huisnummer))
                throw new AdresException("ZetHuisnummer - huisnummer is null of leeg");

            if (!char.IsDigit(huisnummer[0]))
                throw new AdresException("ZetHuisnummer - huisnummer begint niet met getal");
            Huisnummer = huisnummer.Trim();
        }

        /// <summary>
        /// Veranderd de stad van het adres.
        /// </summary>
        /// <param name="stad">De stad van het adres.</param>
        public void ZetStad(string stad)
        {
            if (string.IsNullOrWhiteSpace(stad)) throw new AdresException("ZetAdres - stad is null of leeg");
            Stad = stad.Trim().ToUpper();
        }

        /// <summary>
        /// Veranderd de postocode van het adres
        /// </summary>
        /// <param name="postcode">De postcode van de bestuurder</param>
        public void ZetPostcode(string postcode)
        {
            if (string.IsNullOrWhiteSpace(postcode))
                throw new AdresException("ZetPostcoder - postcode is null of leef");
            Postcode = postcode.Trim();
        }

        /// <summary>
        /// Veranderd het land van de bestuurder.
        /// </summary>
        /// <param name="land">Het land van de bestuurder.</param>
        public void ZetLand(string land)
        {
            if (string.IsNullOrWhiteSpace(land)) throw new AdresException("ZetLand - land is null of leef");
            Land = land.Trim().ToUpper();
        }

        /// <summary>
        /// Controlleert of 2 adressen hetzelfde zijn 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)//Todo: tests schrijven
        {
            return obj is Adres other && Id == other.Id && Straat == other.Straat && Huisnummer == other.Huisnummer && Busnummer == other.Busnummer && Stad == other.Stad && Postcode == other.Postcode && Land == other.Land;
        }

        /// <summary>
        /// Geeft de hashcode voor een adres
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Straat, Huisnummer, Busnummer, Stad, Postcode, Land);
        }
    }
}