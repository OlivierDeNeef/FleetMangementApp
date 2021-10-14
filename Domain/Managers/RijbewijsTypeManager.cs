using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;

namespace DomainLayer.Managers
{
    public class RijbewijsTypeManager
    {
        private readonly IRijbewijsTypeRepo _rijbewijsTypeRepo;

        public RijbewijsTypeManager(IRijbewijsTypeRepo rijbewijsTypeRepo)
        {
            _rijbewijsTypeRepo = rijbewijsTypeRepo;
        }

        public void VoegRijbewijsTypeToe(RijbewijsType rijbewijsType)
        {
            try
            {
                if (!_rijbewijsTypeRepo.BestaatRijbewijsType(rijbewijsType))
                {
                    _rijbewijsTypeRepo.VoegRijbewijsToe(rijbewijsType);
                }
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("VoegRijbewijsTypeToe - Er ging iets mis met het toevoegen van een rijbewijstype", e);
            }
        }

        public void VerwijderRijbewijsType(RijbewijsType rijbewijsType)
        {
            try
            {
                if (_rijbewijsTypeRepo.BestaatRijbewijsType(rijbewijsType))
                {
                    _rijbewijsTypeRepo.VerwijderRijbewijsType(rijbewijsType);
                }
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("VerwijderRijbewijsType - Er ging iets mis met het verwijderern van een rijbewijstype", e);
            }
        }

        public void UpdateRijbewijsType(RijbewijsType rijbewijsType)
        {
            try
            {
                if (_rijbewijsTypeRepo.BestaatRijbewijsType(rijbewijsType))
                {
                    _rijbewijsTypeRepo.UpdateRijbewijsType(rijbewijsType);
                }
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("UpdateRijbewijsType - Er ging iets mis met het updaten van een rijbewijstype", e);
            }
        }

        public IEnumerable<RijbewijsType> GeefAlleRijsbewijsTypes()
        {
            try
            {
                return _rijbewijsTypeRepo.GeefAlleRijbewijsTypes();
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("GeefAlleRijbewijsTypes - Er is iets mis", e);
            }
        }
    }
}