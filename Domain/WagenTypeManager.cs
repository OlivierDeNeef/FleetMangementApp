using DomainLayer.Interfaces;

namespace DomainLayer
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