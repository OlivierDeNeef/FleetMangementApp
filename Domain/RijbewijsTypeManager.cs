using DomainLayer.Interfaces;

namespace DomainLayer
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