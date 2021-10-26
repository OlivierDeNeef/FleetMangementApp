using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface IBrandstofTypeRepo
    {
        BrandstofType VoegBrandstofTypeToe(BrandstofType brandstofType);
        void VerwijderBrandstofType(BrandstofType brandstofType);
        void UpdateBrandstofType(BrandstofType brandstofType);
        IEnumerable<BrandstofType> GeefAlleBrandstofTypes();
        bool BestaatBrandstofType(BrandstofType brandstofType);

    }
}