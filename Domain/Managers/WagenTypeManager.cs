using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using DomainLayer.Exceptions.Managers;
using System;
using System.Collections.Generic;

namespace DomainLayer.Managers
{
    public class WagenTypeManager
    {
        private readonly IWagenTypeRepo _wagenTypeRepo;

        public WagenTypeManager(IWagenTypeRepo wagenTypeRepo)
        {
            _wagenTypeRepo = wagenTypeRepo;
        }

        /// <summary>
        /// Voeg een wagentype toe aan de data source
        /// </summary>
        /// <param name="wagenType">wagentype dat moet toegevoegd worden</param>
        public void VoegWagenTypeToe(WagenType wagenType)
        {
            try
            {
                if (!_wagenTypeRepo.BestaatWagenType(wagenType))
                {
                    _wagenTypeRepo.VoegWagenTypeToe(wagenType);
                }
            }
            catch (Exception e)
            {

                throw new WagenTypeManagerException("VoegWagenTypeToe - Er ging iets mis bij het toevoegen",e);
            }
        }

        /// <summary>
        /// Verwijder een wagentype uit de data source
        /// </summary>
        /// <param name="wagenType">wagentype dat moet verwijderd worden</param>
        public void VerwijderWagenType(WagenType wagenType)
        {
            try
            {
                if (_wagenTypeRepo.BestaatWagenType(wagenType))
                {
                    _wagenTypeRepo.VerwijderWagenType(wagenType.Id);
                }
            }
            catch (Exception e)
            {
                throw new WagenTypeManagerException("VerwijderWagenType - Er ging iets mis bij het verwijderen",e);
            }
        }

        /// <summary>
        /// Update een wagentype uit de data source
        /// </summary>
        /// <param name="wagenType">het up te daten wagentype</param>
        public void UpdateWagenType(WagenType wagenType)
        {
            try
            {
                if (_wagenTypeRepo.BestaatWagenType(wagenType))
                {
                    _wagenTypeRepo.UpdateWagenType(wagenType);
                }
            }
            catch (Exception e)
            {
                throw new WagenTypeManagerException("UpdateWagenType - Er ging iets mis bij het updaten",e);
            }
        }

        /// <summary>
        /// Geeft een overzicht van alle wagentypes
        /// </summary>
        /// <returns>een IEnumerable van Wagentypes</returns>
        public IEnumerable<WagenType> GeefAlleWagenTypes()
        {
            try
            {
                return _wagenTypeRepo.GeefAlleWagenTypes();
            }
            catch (Exception e)
            {
                throw new WagenTypeManagerException("GeefAlleWagenTypes - Er ging iets mis bij het opvragen van de wagentypes",e);
            }
        }
    }
}