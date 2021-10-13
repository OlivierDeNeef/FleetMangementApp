using System;
using System.Linq;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Utilities;

namespace DomainLayer.Utilities
{
    public static class RijksregisternummerChecker
    {

        /// <summary>
        /// Controlleert het rijksregisternummer en geeft een geformateert rijksregisternummer terug.
        /// We controleren het rijksregisternummer via de volgende regels --> https://nl.wikipedia.org/wiki/Rijksregisternummer#Samenstelling
        /// </summary>
        /// <param name="rijksregisternummer"></param>
        /// <param name="geboortedatum"></param>
        /// <returns>Geeft een geformateerde rijksregisternummer terug</returns>
        public static string Parse(string rijksregisternummer, DateTime geboortedatum ) //ASK: Deze manier goed ?
        {
            if (string.IsNullOrEmpty(rijksregisternummer.Trim())) throw new RijksregisternummerCheckerException($"{nameof(rijksregisternummer)} kan niet leeg of null zijn.");
            rijksregisternummer = rijksregisternummer.Trim().Replace(".", "").Replace("-", "").Replace(" ", "");
            if (rijksregisternummer.Length != 11) throw new RijksregisternummerCheckerException($"Het {nameof(rijksregisternummer)} moet 11 karakters hebben");
            if (!rijksregisternummer.All(char.IsDigit)) throw new RijksregisternummerCheckerException($"Het {nameof(rijksregisternummer)} kan alleen maar cijfer bevatten");
            if (geboortedatum.ToString("yyMMdd") != rijksregisternummer[..6]) throw new RijksregisternummerCheckerException($"Het {rijksregisternummer} komt niet overeen met de geboortedatum");
            var tweedeDeel = int.Parse(rijksregisternummer.Substring(6, 3));
            if (1 > tweedeDeel || tweedeDeel > 998) throw new RijksregisternummerCheckerException($"Het {nameof(rijksregisternummer)} heeft niet het juist formaat");
            var aaneengeschakeldGetal = geboortedatum.Year > 1999 ? int.Parse("2" + rijksregisternummer.Substring(0, 9)) : int.Parse(rijksregisternummer.Substring(0, 9));
            var controlGetal = 97 - (aaneengeschakeldGetal % 97);
            if (controlGetal.ToString() != rijksregisternummer.Substring(9, 2)) throw new RijksregisternummerCheckerException($"Het {nameof(rijksregisternummer)} is ongeldig het controle getal klopt niet");
            return  rijksregisternummer;
        }
    }
}