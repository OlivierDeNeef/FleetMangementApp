using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System.Collections.Generic;

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
             _brandstofTypeRepo.VoegBrandstofTypeToe(brandstofType);
        }

        /// <summary>
        /// Verwijder een brandstof type van de data source
        /// </summary>
        /// <param name="brandstofType">Brandstof dat verwijderd moet worden</param>
        public void VerwijderBrandstofType(BrandstofType brandstofType)
        {
            if (_brandstofTypeRepo.BestaatBrandstofType(brandstofType)) _brandstofTypeRepo.VerwijderBrandstofType(brandstofType.Id);
        }

        /// <summary>
        /// Update een brandstof in de data source
        /// </summary>
        /// <param name="brandstofType">Brandstof dat geupdate moet worden</param>
        public void UpdateBrandstofType(BrandstofType brandstofType)
        {
            if (_brandstofTypeRepo.BestaatBrandstofType(brandstofType)) _brandstofTypeRepo.UpdateBrandstofType(brandstofType);
        }

        /// <summary>
        /// Geeft alle brandstoftype van de data source
        /// </summary>
        /// <returns>List van brandstoffen</returns>
        public IEnumerable<BrandstofType> GeefAlleBrandstofTypes()
        {
            return _brandstofTypeRepo.GeefAlleBrandstofTypes();
        }
    }
}