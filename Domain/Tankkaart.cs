using System;
using System.Collections.Generic;
using DomainLayer.Exceptions;

namespace DomainLayer
{
    public class Tankkaart
    {
        public int Id { get; private set; } // verplicht
        public string Kaartnummer { get; private set; } //verplicht
        public DateTime Geldigheidsdatum { get; private set; } //verplicht
        public int Pincode { get; private set; }
        private List<BrandstofType> _brandstofTypes = new();
        public Bestuurder Bestuurder { get; private set; }
        public bool IsGeblokkeerd { get; private set; } = false;

        public Tankkaart(int id, string kaartnummer, DateTime geldigheidsDatum)
        {
            ZetId(id);
            ZetKaartnummer(kaartnummer);
            ZetGeldigheidsdatum(geldigheidsDatum);
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

        public void ZetPincode(int pincode) //moet 4 cijfers zijn
        {
            
            if(pincode>9999) throw new TankkaartException("pincode mag maar 4 cijfers bevatten");
            if(pincode < 1000) throw new TankkaartException("pincode moet 4 cijfers bevatten");
            
            Pincode = pincode;
        }

        
        public void ZetGeldigheidsdatum(DateTime datum)
        {
            Geldigheidsdatum = datum;
        }

        public void VoegBrandstofTypeToe(BrandstofType brandstof)
        {
            if(brandstof == null) throw new TankkaartException("AddBrandstofType - brandstof is null");

            if(_brandstofTypes.Contains(brandstof))
                throw new TankkaartException("brandstofType bestaat al");
            else
                _brandstofTypes.Add(brandstof);
        }

        public IReadOnlyList<BrandstofType> GeefBrandstofTypes()
        {
            return _brandstofTypes;
        }

        public void VerwijderBrandstofType(BrandstofType type)
        {
            if(type == null) throw new TankkaartException("VerwijderBrandstofType - type is null");
            if(!_brandstofTypes.Contains(type)) throw new TankkaartException("VerwijderBrandstofType - type zit niet op deze tankkaart");
            _brandstofTypes.Remove(type);
        }

        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if(bestuurder == null) throw new TankkaartException("SetBestuurder - bestuurder is null ");
            if(Bestuurder != null)
            {
                Bestuurder.RemoveTankkaart();
            }
            Bestuurder = bestuurder;
        }

        public void BlokkeerKaart()
        {
            if(IsGeblokkeerd) throw new TankkaartException("kaart is al geblokkeerd");
            IsGeblokkeerd = true;

        }
    }
}