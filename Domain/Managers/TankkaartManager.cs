using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;

namespace DomainLayer.Managers
{
    public class TankkaartManager
    {
        private readonly ITankkaartRepo _tankkaartRepo;

        public TankkaartManager(ITankkaartRepo tankkaartRepo)
        {
            _tankkaartRepo = tankkaartRepo;
        }

        public void VoegTankkaartToe(Tankkaart tankkaart)
        {
            _tankkaartRepo.VoegTankkaartToe(tankkaart);
        }

        public Tankkaart GeefTankkaart(int id)
        {
            if (!_tankkaartRepo.BestaatTankkaart(id))
                throw new TankkaartManagerException("GeefTankkaart - tankkaart bestaat niet");
            return _tankkaartRepo.GeefTankkaart(id);
        }

        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten(string kaartnummer, DateTime geldigheidsdatum, List<BrandstofType> lijstBrandstoftypes, bool geachiveerd)
        {
            try
            {
                return _tankkaartRepo.GeefGefilterdeTankkaarten(kaartnummer, geldigheidsdatum, lijstBrandstoftypes, geachiveerd);

            }
            catch (Exception e)
            {
                throw new VoertuigManagerException(nameof(GeefGefilterdeTankkaarten) + " Er ging iets mis", e);

            }

        }

        public void UpdateTankkaart(Tankkaart tankkaart)
        {
            if(_tankkaartRepo.BestaatTankkaart(tankkaart.Id))_tankkaartRepo.UpdateTankkaart(tankkaart);
        }
    }
}