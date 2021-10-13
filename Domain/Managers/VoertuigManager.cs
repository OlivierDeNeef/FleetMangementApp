using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;

namespace DomainLayer.Managers
{
    public class VoertuigManager
    {
        private readonly IVoertuigRepo _voertuigRepo;

        public VoertuigManager(IVoertuigRepo voertuigRepo)
        {
            _voertuigRepo = voertuigRepo;
        }
    }
}