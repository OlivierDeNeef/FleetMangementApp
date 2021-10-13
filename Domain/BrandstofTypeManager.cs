using DomainLayer.Interfaces;

namespace DomainLayer
{
    public class BrandstofTypeManager
    {
        private readonly IBrandstofTypeRepo _brandstofTypeRepo;

        public BrandstofTypeManager(IBrandstofTypeRepo brandstofTypeRepo)
        {
            _brandstofTypeRepo = brandstofTypeRepo;
        }
    }
}