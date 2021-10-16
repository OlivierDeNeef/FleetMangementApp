using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    /// <summary>
    /// Todo : isGerarchiveerd toevoegen + methodes
    /// ASK : Kaart kan niet gedeblokkeerd worden?
    /// ASK : ZetBrandstofTypes hebben we deze methode nodige?
    /// </summary>


    public class Tankkaart
    {
        public int Id { get; private set; } // verplicht
        public string Kaartnummer { get; private set; } //verplicht
        public DateTime Geldigheidsdatum { get; private set; } //verplicht
        public string Pincode { get; private set; }
        private readonly List<BrandstofType> _brandstofTypes = new();
        public Bestuurder Bestuurder { get; private set; }
        public bool IsGeblokkeerd { get; private set; }

        public Tankkaart(int id, string kaartnummer, DateTime geldigheidsDatum)
        {
            ZetId(id);
            ZetKaartnummer(kaartnummer);
            ZetGeldigheidsdatum(geldigheidsDatum);
        }
        public Tankkaart(int id, string kaartnummer, DateTime geldigheidsDatum, string pincode, Bestuurder bestuurder) : this(id, kaartnummer, geldigheidsDatum)
        {
            ZetPincode(pincode);
            ZetBestuurder(bestuurder);
            
        }
        public void ZetId(int id)
        {
            if(id <= 0) throw new TankkaartException("Id mag niet kleiner zijn dan 1");
            Id = id;
        }

        public void ZetKaartnummer(string kaartnummer)
        {
            if(String.IsNullOrWhiteSpace(kaartnummer)) throw new TankkaartException("Het kaartnummer mag niet leeg zijn");
            Kaartnummer = kaartnummer.Trim();
        }

        public void ZetPincode(string pincode) //moet 4 cijfers zijn
        {
            
            if (!int.TryParse(pincode, out int pinAsNumber)) throw new TankkaartException("pincode mag enkel cijfers bevatten");
            if(pincode.Length > 4) throw new TankkaartException("pincode mag maar 4 cijfers bevatten");
            if(pincode.Length < 4) throw new TankkaartException("pincode moet 4 cijfers bevatten");
            
            Pincode = pincode;
        }

        
        public void ZetGeldigheidsdatum(DateTime datum)
        {
            Geldigheidsdatum = datum;
        }

        public void VoegBrandstofTypeToe(BrandstofType brandstof)
        {
            if(brandstof == null) throw new TankkaartException("VoegBrandstofTypeToe - brandstof is null");

            if(_brandstofTypes.Contains(brandstof))
                throw new TankkaartException("brandstofType zit al in de tankkaart");

            if (_brandstofTypes.Select(b => b.Type == brandstof.Type).Any()) throw new TankkaartException("VoegBrandstofTypeToe - er is al een brandstof van dit type");
            
            _brandstofTypes.Add(brandstof);
        }

        public IReadOnlyList<BrandstofType> GeefBrandstofTypes()
        {
            return _brandstofTypes;
        }

        /*public bool HeeftBrandstofType(BrandstofType type)
        {
            //if(type == null)
        }*/

        public void VerwijderBrandstofType(BrandstofType type)
        {
            if(type == null) throw new TankkaartException("VerwijderBrandstofType - type is null");
            if(!_brandstofTypes.Contains(type)) throw new TankkaartException("VerwijderBrandstofType - type zit niet op deze tankkaart");
            _brandstofTypes.Remove(type);
        }

        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if(bestuurder == null) throw new TankkaartException("ZetBestuurder - bestuurder is null ");
            if(Bestuurder?.Tankkaart != null)
            {
                Bestuurder.VerwijderTankkaart();
            }
            Bestuurder = bestuurder;
            if(bestuurder.Tankkaart != this) bestuurder.ZetTankkaart(this);
        }

        public void BlokkeerKaart()
        {
            if(IsGeblokkeerd) throw new TankkaartException("BlokkeerKaart - kaart is al geblokkeerd");
            IsGeblokkeerd = true;
        }

        public void DeblokkeerKaart()
        {
            if (!IsGeblokkeerd) throw new TankkaartException("DeblokkeerKaart - kaart is niet geblokkeerd");
            IsGeblokkeerd = false;
        }

        public void VerwijderBestuurder()
        {
            if (Bestuurder == null) throw new TankkaartException("VerwijderBestuurder - Bestuurder is al null");
            Bestuurder = null;
        }
    }
}