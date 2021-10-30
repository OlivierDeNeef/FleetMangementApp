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

        /// <summary>
        /// Voegt een bestuurder toe aan data source
        /// </summary>
        /// <param name="bestuurderRepo"></param>
        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            try
            {
                if (!_bestuurderRepo.BestaatBestuurder(bestuurder))
                {
                    _bestuurderRepo.VoegBestuurderToe(bestuurder);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new BestuurderException("VoegBestuurderToe - Er ging iets mis bij het toevoegen", e);
            }
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            try
            {
                if(_bestuurderRepo.BestaatBestuurder(bestuurder))
                    _bestuurderRepo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}