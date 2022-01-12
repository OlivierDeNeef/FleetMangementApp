using System;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System.Collections.Generic;
using DomainLayer.Exceptions.Managers;

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
             _brandstofTypeRepo.VoegBrandstofTypeToe(brandstofType);

            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("Er ging iets mis", e);
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
                if (_brandstofTypeRepo.BestaatBrandstofType(brandstofType)) _brandstofTypeRepo.VerwijderBrandstofType(brandstofType.Id);
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("Er ging iets mis", e);
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
                if (_brandstofTypeRepo.BestaatBrandstofType(brandstofType)) _brandstofTypeRepo.UpdateBrandstofType(brandstofType);
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("Er ging iets mis", e);
            }
        }

        /// <summary>
        /// Geeft alle brandstoftype van de data source
        /// </summary>
        /// <returns>List van brandstoffen</returns>
        public IEnumerable<BrandstofType> GeefAlleBrandstofTypes()
        {
            try
            {
                return _brandstofTypeRepo.GeefAlleBrandstofTypes();
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("Er ging iets mis", e);
            }
        }
    }
}