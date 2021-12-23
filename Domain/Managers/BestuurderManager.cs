using DomainLayer.Exceptions.Managers;
using DomainLayer.Exceptions.Models;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;

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
            _bestuurderRepo.VoegBestuurderToe(bestuurder);
        }
        /// <summary>
        /// dit verwijdert een bestuurder
        /// </summary>
        /// <param name="id"></param>
        public void VerwijderBestuurder(int id)
        {
            if (_bestuurderRepo.BestaatBestuurder(id)) _bestuurderRepo.VerwijderBestuurder(id);
        }

        /// <summary>
        /// update een bestuurder 
        /// </summary>
        /// <param name="bestuurder"></param>
        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            if (_bestuurderRepo.BestaatBestuurder(bestuurder.Id)) _bestuurderRepo.UpdateBestuurder(bestuurder);
        }

        /// <summary>
        /// Geeft een list van bestuurders die bij de filter horen.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="voornaam"></param>
        /// <param name="naam"></param>
        /// <param name="id"></param>
        /// <param name="geboortedatum"></param>
        /// <param name="lijstRijbewijstypes"></param>
        /// <param name="rijksregisternummer"></param>
        /// <param name="gearchiveerd"></param>
        public IReadOnlyList<Bestuurder> GeefGefilterdeBestuurder(int id, string voornaam, string naam, DateTime geboortedatum, List<RijbewijsType> lijstRijbewijstypes, string rijksregisternummer, bool gearchiveerd)
        {
            var lijstBestuurders = new List<Bestuurder>();
            if (id <= 0) return _bestuurderRepo.GeefGefilderdeBestuurders(voornaam, naam, geboortedatum, lijstRijbewijstypes, rijksregisternummer, gearchiveerd);
            lijstBestuurders.Add(_bestuurderRepo.GeefBestuurder(id));
            return lijstBestuurders;
        }

        public bool BestaatBestuurder(Bestuurder bestuurder)
        {
            if (_bestuurderRepo.BestaatBestuurder(bestuurder.Id)) throw new BestuurderManagerException("Bestaat bestuurder - Bestuurder bestaat al");
            return _bestuurderRepo.BestaatBestuurder(bestuurder.Id);
        }

        /// <summary>
        /// geeft een bestuurder  
        /// </summary>
        /// <param name="bestuurder"></param>
        /// <returns></returns>
        public Bestuurder GeefBestuurder(int id)
        {
            if (!_bestuurderRepo.BestaatBestuurder(id)) throw new BestuurderManagerException("BestaatBestuurder - Bestuurder bestaat niet");
            return _bestuurderRepo.GeefBestuurder(id);
        }
    }
}