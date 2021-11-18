using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;

namespace DomainLayer.Managers
{
    public class TankkaartManager
    {
        private readonly ITankkaartRepo _tankkaartRepo;

        public TankkaartManager(ITankkaartRepo tankkaartRepo)
        {
            _tankkaartRepo = tankkaartRepo;
        }

        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten([Optional] string kaartnummer,
            [Optional] DateTime geldigheidsdatum, [Optional] List<BrandstofType> lijstBrandstoftypes,
            [Optional] bool geachiveerd)
        {
            
            try
            {
                
                return _tankkaartRepo.GeefGefilterdeTankkaarten(kaartnummer,geldigheidsdatum,lijstBrandstoftypes,geachiveerd);


    }
}