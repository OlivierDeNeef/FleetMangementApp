using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DomainLayer.Managers
{
    public class TankkaartManager
    {
        private readonly ITankkaartRepo _tankkaartRepo;

        public TankkaartManager(ITankkaartRepo tankkaartRepo)
        {
            _tankkaartRepo = tankkaartRepo;
        }

        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten([Optional] string kaartnummer,
            [Optional] DateTime geldigheidsdatum, [Optional] List<BrandstofType> lijstBrandstoftypes,
            [Optional] bool geachiveerd)
        {
            var lijstTankkaarten = new List<Tankkaart>();
            try
            {
                if (id > 0)
                {
                    lijstTankkaarten.Add(_voertuigRepo.GeefVoertuig(id));
                    return lijstTankkaarten;
                }
                return _voertuigRepo.GeefGefilterdeVoertuigen(id, merk, model, aantalDeuren, nummerplaat, chassisnummer, kleur, wagenType, brandstofType, gearchiveerd, type);

            }
            catch (Exception e)
            {
                throw new TankkaartManagerException("GeefGefilterdeTankkaarten, er ging iets mis", e);
            }
        }
        public void VoegTankkaartToe(Tankkaart tankkaart)
        {
            try
            {
                if (_tankkaartRepo.BestaatTankkaart(tankkaart.Id))
                    _tankkaartRepo.VoegTankkaartToe(tankkaart);
            }
            catch
            {
                throw new TankkaartManagerException("VoegTankkaartToe - er ging iets mis");
            }
        }
        public void UpdateTankkaart(Tankkaart tankkaart)
        {
            try
            {
                if (_tankkaartRepo.BestaatTankkaart(tankkaart.Id))
                    _tankkaartRepo.UpdateTankkaart(tankkaart);
            }
            catch
            {
                throw new TankkaartManagerException("UpdateTankkaart - er ging iets mis");
            }
        }
        
        public Tankkaart GeefTankkaart(int id)
        {
            try
            {
                    return _tankkaartRepo.GeefTankkaart(id);
            }
            catch
            {
                throw new TankkaartManagerException("GeefTankkaart - er ging iets mis");
            }
        }
    }
}