using System;
using DomainLayer.Exceptions.Managers;
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
        /// <param name="bestuurder"></param>
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
                throw new BestuurderException("VoegBestuurderToe - Er ging iets mis bij het toevoegen", e);
            }
        }
        /// <summary>
        /// verwijderd een bestuurder
        /// </summary>
        /// <param name="bestuurder"></param>
        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            try
            {
                if(_bestuurderRepo.BestaatBestuurder(bestuurder))
                    _bestuurderRepo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("VerijderBestuurder - Er ging iets mis", e);
            }

        }

        /// <summary>
        /// update een bestuurder 
        /// </summary>
        /// <param name="bestuurder"></param>
        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            try
            {
                if (_bestuurderRepo.BestaatBestuurder(bestuurder))
                {
                    _bestuurderRepo.UpdateBestuurder(bestuurder);
                }
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("updateBestuurder - Er ging iets mis", e);
            }
        }

        /// <summary>
        /// Geeft een list van bestuurders die bij de filter horen.
        /// </summary>
        /// <param name="b"></param>
        public void GeefGefilterdeBestuurder(Bestuurder b) /// stuck
        {
            //try
            //{ 
            //    if(_bestuurderRepo.BestaatBestuurder(b))
            //    {
            //        _bestuurderRepo.GeefGefilderdeBestuurders(b)
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
        }

        public bool BestaatBestuurder(Bestuurder b)
        {
            //if (_bestuurderRepo.BestaatBestuurder(b))
            //{
            //    throw new BrandstofTypeManagerException("Bestaat bestuurder - Bestuurder bestaat al");
            //}
            
            return _bestuurderRepo.BestaatBestuurder(b);
        }

        public Bestuurder GeefBestuurder(Bestuurder b)
        {
            if (!_bestuurderRepo.BestaatBestuurder(b))
            {
                throw new BrandstofTypeManagerException("BestaatBestuurder - Bestuurder bestaat niet");
            }
            
            return _bestuurderRepo.GeefBestuurder(b.Id);
            
        }
    }
}