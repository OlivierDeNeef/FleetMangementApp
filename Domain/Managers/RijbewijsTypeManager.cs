using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;

namespace DomainLayer.Managers
{
    public class RijbewijsTypeManager
    {
        private readonly IRijbewijsTypeRepo _rijbewijsTypeRepo;

        public RijbewijsTypeManager(IRijbewijsTypeRepo rijbewijsTypeRepo)
        {
            _rijbewijsTypeRepo = rijbewijsTypeRepo;
        }
    }
}