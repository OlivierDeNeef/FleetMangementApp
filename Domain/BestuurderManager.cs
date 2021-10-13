using DomainLayer.Interfaces;

namespace DomainLayer
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