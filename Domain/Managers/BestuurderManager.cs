using System;
using DomainLayer.Exceptions.Models;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;

namespace DomainLayer.Managers
{
    public class BestuurderManager
    {
        private readonly IBestuurderRepo _bestuurderRepo;
        public BestuurderManager(IBestuurderRepo bestuurderRepo)
        {
            _bestuurderRepo = bestuurderRepo;
        }

        public void VoegBestuurderToe(IBestuurderRepo bestuurderRepo)
        {
            try
            {
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new BestuurderException("VoegBestuurderToe - Er ging iets mis bij het toevoegen", e);
            }
        }
    }
}