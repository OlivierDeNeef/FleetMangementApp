using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;

namespace DomainLayer.Managers
{
    public class BestuurderManager
    {
        private readonly IBestuurderRepo _bestuurderRepo;
        public BestuurderManager(IBestuurderRepo bestuurderRepo)
        {
            _bestuurderRepo = bestuurderRepo;
        }
    }
}