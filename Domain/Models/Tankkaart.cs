using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class Tankkaart
    {
        public int Id { get; private set; } // verplicht
        public string Kaartnummer { get; private set; } //verplicht
        public DateTime Geldigheidsdatum { get; private set; } //verplicht
        public string Pincode { get; private set; }
        private readonly List<BrandstofType> _brandstofTypes = new();
        public Bestuurder Bestuurder { get; private set; }
        public bool IsGeblokkeerd { get; private set; }
        public bool IsGearchiveerd { get; set; }


        public Tankkaart(string kaartnummer, DateTime geldigheidsDatum)//Todo: tests schrijven
        {
            ZetKaartnummer(kaartnummer);
            ZetGeldigheidsdatum(geldigheidsDatum);
        }

        public Tankkaart(int id, string kaartnummer, DateTime geldigheidsDatum) : this(kaartnummer, geldigheidsDatum)
        {
            ZetId(id);
        }
        public Tankkaart(int id, string kaartnummer, DateTime geldigheidsDatum, string pincode, Bestuurder bestuurder) : this(id, kaartnummer, geldigheidsDatum)
        {
            ZetPincode(pincode);
            if (bestuurder != null)
            {
                 ZetBestuurder(bestuurder);
            }
        }

        public Tankkaart(string kaartnummer, DateTime geldigheidsDatum, string pincode, Bestuurder bestuurder, bool isGeblokkeerd, bool isGeachiveerd, List<BrandstofType> brandstofTypes): this(kaartnummer,geldigheidsDatum)
        {
            ZetPincode(pincode);
            if (bestuurder != null)
            {
                ZetBestuurder(bestuurder);
            }
            if (isGeachiveerd)
            {
                IsGearchiveerd = true;
            }

            if (isGeblokkeerd)
            {
                BlokkeerKaart();
            }
            _brandstofTypes = brandstofTypes;
        }

        public Tankkaart(int id, string kaartnummer, DateTime geldigheidsDatum, string pincode, Bestuurder bestuurder, bool isGeblokkeerd,bool isGeachiveerd , List<BrandstofType> brandstofTypes) : this(id, kaartnummer, geldigheidsDatum, pincode, bestuurder)
        {
            if (isGeachiveerd)
            {
                IsGearchiveerd = true;
            }

            if (isGeblokkeerd)
            {
                BlokkeerKaart();
            }
            _brandstofTypes = brandstofTypes;
        }


        /// <summary>
        /// Check en zet het id van de tankkaart
        /// </summary>
        /// <param name="id">TankkaartId</param>
        public void ZetId(int id)
        {
            if(id <= 0) throw new TankkaartException("Id mag niet kleiner zijn dan 1");
            Id = id;
        }

        /// <summary>
        /// Check van he kaartnummer
        /// </summary>
        /// <param name="kaartnummer">Kaartnummer van de tankkaart</param>
        public void ZetKaartnummer(string kaartnummer)
        {
            if(string.IsNullOrWhiteSpace(kaartnummer)) throw new TankkaartException("Het kaartnummer mag niet leeg zijn");
            Kaartnummer = kaartnummer.Trim();
        }


        /// <summary>
        /// check van de pincode van de tankkaart
        /// </summary>
        /// <param name="pincode">pincode van de tankkaart</param>
        public void ZetPincode(string pincode) //moet 4 cijfers zijn
        {
            
            if (!int.TryParse(pincode, out int pinAsNumber)) throw new TankkaartException("pincode mag enkel cijfers bevatten");
            if(pincode.Length > 4) throw new TankkaartException("pincode mag maar 4 cijfers bevatten");
            if(pincode.Length < 4) throw new TankkaartException("pincode moet 4 cijfers bevatten");
            
            Pincode = pincode;
        }

        /// <summary>
        /// Zet de geldigheidsdatum van de tankkaart
        /// </summary>
        /// <param name="datum">geldigheidsdatum van de tankkkaart</param>
        public void ZetGeldigheidsdatum(DateTime datum)
        {
            Geldigheidsdatum = datum;
        }


        /// <summary>
        /// Voegt een geldig brandstoftype toe aan de tankkkaart
        /// </summary>
        /// <param name="brandstof">Brandstoftype waarmee de tankkaart kan tanken</param>
        public void VoegBrandstofTypeToe(BrandstofType brandstof)
        {
            if(brandstof == null) throw new TankkaartException("VoegBrandstofTypeToe - brandstof is null");

            if(_brandstofTypes.Contains(brandstof))
                throw new TankkaartException("brandstofType zit al in de tankkaart");

            if (_brandstofTypes.Select(b => b.Type == brandstof.Type).Any()) throw new TankkaartException("VoegBrandstofTypeToe - er is al een brandstof van dit type");
            
            _brandstofTypes.Add(brandstof);
        }

        /// <summary>
        /// Geeft de brandstoftypes van de tankkaart
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<BrandstofType> GeefBrandstofTypes()//Todo: tests schrijven
        {
            return _brandstofTypes;
        }

        /// <summary>
        /// Controlleert of de tankkaart een brandstoftype bevat
        /// 
        /// </summary>
        /// <param name="type">brandstoftype van de tankkaart</param>
        /// <returns></returns>
        public bool HeeftBrandstofType(BrandstofType type)
        {
            if (type == null)
                throw new TankkaartException("HeeftBrandstoftype - er ging iets mis met de check");

            return _brandstofTypes.Contains(type);
        }

        /// <summary>
        /// Archiveerd een bestuurder van een tankkaart en omgekeerd
        /// </summary>
        /// <param name="isGearchiveerd"></param>
        public void ZetGearchiveerd(bool isGearchiveerd)
        {
            if (isGearchiveerd && Bestuurder != null)
            {
                VerwijderBestuurder();
            }
            IsGearchiveerd = isGearchiveerd;
        }


        /// <summary>
        /// Verwijdert een brandstoftype van een tankkaart
        /// </summary>
        /// <param name="type">Brandstoftype die verwijderd wordt</param>
        public void VerwijderBrandstofType(BrandstofType type)
        {
            if(type == null) throw new TankkaartException("VerwijderBrandstofType - type is null");
            if(!_brandstofTypes.Contains(type)) throw new TankkaartException("VerwijderBrandstofType - type zit niet op deze tankkaart");
            _brandstofTypes.Remove(type);
        }


        /// <summary>
        /// Voegt een bestuurder toe aan een tankkaart
        /// </summary>
        /// <param name="bestuurder">Bestuurder waartoe de tankkaart is toegewezen</param>
        public void ZetBestuurder(Bestuurder bestuurder)//Todo: tests uitbereiden
        {
            if(bestuurder == null) throw new TankkaartException("ZetBestuurder - bestuurder is null ");
            if(Bestuurder?.Tankkaart != null)
            {
                Bestuurder.VerwijderTankkaart();
            }
            Bestuurder = bestuurder;
            if(!Equals(bestuurder.Tankkaart, this)) bestuurder.ZetTankkaart(this);
        }


        /// <summary>
        /// status van het blokkeeren van een kaart 
        /// </summary>
        public void BlokkeerKaart()
        {
            if(IsGeblokkeerd) throw new TankkaartException("BlokkeerKaart - kaart is al geblokkeerd");
            IsGeblokkeerd = true;
        }

        /// <summary>
        /// status van het deblokkeeren van een kaart 
        /// </summary>
        public void DeblokkeerKaart()
        {
            if (!IsGeblokkeerd) throw new TankkaartException("DeblokkeerKaart - kaart is niet geblokkeerd");
            IsGeblokkeerd = false;
        }


        /// <summary>
        /// verwijderd een bestuurder van een tankkaart
        /// </summary>
        public void VerwijderBestuurder()
        {
            if (Bestuurder == null) throw new TankkaartException("VerwijderBestuurder - Bestuurder is al null");
            var bestuurder = Bestuurder;
            Bestuurder = null;
            if (bestuurder.Tankkaart != null) bestuurder.VerwijderTankkaart();
        }

        /// <summary>
        /// Controlleerd of 2 tankkaarten hetzelfde zijn 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)//Todo: tests schrijven
        {
            
            var result = obj is Tankkaart other && _brandstofTypes.All(other._brandstofTypes.Contains) &&
                         _brandstofTypes.Count == other._brandstofTypes.Count && Id == other.Id &&
                         Kaartnummer == other.Kaartnummer && Geldigheidsdatum.Equals(other.Geldigheidsdatum) &&
                         Pincode == other.Pincode && Equals(Bestuurder, other.Bestuurder) &&
                         IsGeblokkeerd == other.IsGeblokkeerd && IsGearchiveerd == other.IsGearchiveerd;
             return result;
        }

        /// <summary>
        /// Geeft de hashcode van een tankkaart
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_brandstofTypes, Id, Kaartnummer, Geldigheidsdatum, Pincode, Bestuurder, IsGeblokkeerd, IsGearchiveerd);
        }
    }
}