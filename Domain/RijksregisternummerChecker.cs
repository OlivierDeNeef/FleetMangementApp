using System;
using System.Linq;
using DomainLayer.Exceptions;

namespace DomainLayer
{
    public class RijksregisternummerChecker
    {
        public void Check(string rijksregisternummer, DateTime geboortedatum , out string formattedRijksregisternummer)
        {
            if (string.IsNullOrEmpty(rijksregisternummer.Trim())) throw new BestuurderException($"{nameof(rijksregisternummer)} kan niet leeg of null zijn.");
            var cleanRijksregisternummer = rijksregisternummer.Trim().Replace(".", "").Replace("-", "").Replace(" ", "");
            if (cleanRijksregisternummer.Length != 11) throw new BestuurderException($"Het {nameof(rijksregisternummer)} moet 11 karakters hebben");
            if (!cleanRijksregisternummer.All(char.IsDigit)) throw new BestuurderException($"Het {nameof(rijksregisternummer)} kan alleen maar cijfer bevatten");
            if (geboortedatum.ToString("yyMMdd") != cleanRijksregisternummer.Substring(0, 6)) throw new BestuurderException($"Het {rijksregisternummer} komt niet overeen met de geboortedatum");

            var tweedeDeel = int.Parse(cleanRijksregisternummer.Substring(6, 3));
            if (1 > tweedeDeel || tweedeDeel > 998) throw new BestuurderException($"Het {nameof(rijksregisternummer)} heeft niet het juist formaat");


            var aaneengeschakeldGetal = geboortedatum.Year > 1999 ? int.Parse("2" + cleanRijksregisternummer.Substring(0, 9)) : int.Parse(cleanRijksregisternummer.Substring(0, 9));

            var controlGetal = 97 - (aaneengeschakeldGetal % 97);
            if (controlGetal.ToString() != cleanRijksregisternummer.Substring(9, 2)) throw new BestuurderException($"Het {nameof(rijksregisternummer)} is ongeldig het controle getal klopt niet");
            
            formattedRijksregisternummer = cleanRijksregisternummer;
        }
    }
}