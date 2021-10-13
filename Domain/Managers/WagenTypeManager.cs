using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;

namespace DomainLayer.Managers
{
    public class WagenTypeManager
    {
        private readonly IWagenTypeRepo _wagenTypeRepo;

        public WagenTypeManager(IWagenTypeRepo wagenTypeRepo)
        {
            _wagenTypeRepo = wagenTypeRepo;
        }
    }
}