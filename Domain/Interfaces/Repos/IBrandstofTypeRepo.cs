using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface IBrandstofTypeRepo
    {
        void VoegBrandstofTypeToe(BrandstofType brandstofType);
        void VerwijderBrandstofType(int id);
        void UpdateBrandstofType(BrandstofType brandstofType);
        IEnumerable<BrandstofType> GeefAlleBrandstofTypes();
        bool BestaatBrandstofType(BrandstofType brandstofType);

    }
}