using System;
using System.Collections.Generic;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;

namespace DomainLayer.Managers
{
    public class BrandstofTypeManager
    {
        private readonly IBrandstofTypeRepo _brandstofTypeRepo;

        public BrandstofTypeManager(IBrandstofTypeRepo brandstofTypeRepo)
        {
            _brandstofTypeRepo = brandstofTypeRepo;
        }

        public void VoegBrandstofTypeToe(BrandstofType brandstofType)
        {
            try
            {
                if (!_brandstofTypeRepo.BestaatBrandstofType(brandstofType))
                {
                    _brandstofTypeRepo.VoegBrandstofTypeToe(brandstofType);
                }
            }
            catch (Exception e)
            {
               
                throw new BrandstofTypeManagerException("VoegBrandstofTypeToe - Er ging iets mis bij het toevoegen", e);
            }
        }

        public void VerwijderBrandstofType(BrandstofType brandstofType)
        {
            try
            {
                if (_brandstofTypeRepo.BestaatBrandstofType(brandstofType))
                {
                    _brandstofTypeRepo.VerwijderBrandstofType(brandstofType);
                }
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("VerwijderBrandstofType - Er ging iets mis bij het verwijderen", e);
            }
        }

        public void UpdateBrandstofType(BrandstofType brandstofType)
        {
            try
            {
                if (_brandstofTypeRepo.BestaatBrandstofType(brandstofType))
                {
                    _brandstofTypeRepo.UpdateBrandstofType(brandstofType);
                }
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("UpdateBrandstofType - Er ging iets mis bij het updaten", e);
            }
        }

        public IEnumerable<BrandstofType> GeefAlleBrandstofTypes()
        {
            try
            {
               return _brandstofTypeRepo.GeefAlleBrandstofTypes();
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("GeefAlleBrandstofTypes - Er ging iets mis bij het opvragen van de brandstof types", e);
            }
        }
    }
}