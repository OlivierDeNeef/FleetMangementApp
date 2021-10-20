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

        /// <summary>
        /// Voeg een brandstof type aan de data source
        /// </summary>
        /// <param name="brandstofType">Brandstof dat toegevoegd moet worden</param>
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


        /// <summary>
        /// Verwijder een brandstof type van de data source
        /// </summary>
        /// <param name="brandstofType">Brandstof dat verwijderd moet worden</param>
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

        /// <summary>
        /// Update een brandstof in de data source
        /// </summary>
        /// <param name="brandstofType">Brandstof dat geupdate moet worden</param>
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

        /// <summary>
        /// Geeft alle brandstoftype van de data source
        /// </summary>
        /// <returns>List van branstoffen</returns>
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