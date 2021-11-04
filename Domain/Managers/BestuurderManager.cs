using DomainLayer.Exceptions.Managers;
using DomainLayer.Exceptions.Models;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
                if (!_bestuurderRepo.BestaatBestuurder(bestuurder.Id))
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
                if (_bestuurderRepo.BestaatBestuurder(bestuurder.Id))
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
                if (_bestuurderRepo.BestaatBestuurder(bestuurder.Id))
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
        public IReadOnlyList<Bestuurder> GeefGefilterdeBestuurder([Optional] int id, [Optional] string voornaam, [Optional] string naam,
            [Optional] DateTime geboortedatum, [Optional] List<RijbewijsType> lijstRijbewijstypes, [Optional] string rijksregisternummer, [Optional] bool gearchiveerd) // stuck
        {
            var lijstBestuurders = new List<Bestuurder>();
            try
            {
                if (id > 0)
                {
                    lijstBestuurders.Add(_bestuurderRepo.GeefBestuurder(id));
                    return lijstBestuurders;
                }
                return _bestuurderRepo.GeefGefilderdeBestuurders(voornaam, naam, geboortedatum, lijstRijbewijstypes, // null => lijst rijbewijstypes is private
                     rijksregisternummer, gearchiveerd); //hoe weet die welke parameters ingevuld zijn?

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool BestaatBestuurder(Bestuurder b)
        {
            if (_bestuurderRepo.BestaatBestuurder(b.Id))
            {
                throw new BrandstofTypeManagerException("Bestaat bestuurder - Bestuurder bestaat al");
            }

            return _bestuurderRepo.BestaatBestuurder(b.Id);
        }

        /// <summary>
        /// geeft een bestuurder  
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Bestuurder GeefBestuurder(Bestuurder b)
        {
            if (!_bestuurderRepo.BestaatBestuurder(b.Id))
            {
                throw new BrandstofTypeManagerException("BestaatBestuurder - Bestuurder bestaat niet");
            }

            return _bestuurderRepo.GeefBestuurder(b.Id);

        }
    }
}