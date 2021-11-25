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
            _wagenTypeRepo.VoegWagenTypeToe(wagenType);
        }

        /// <summary>
        /// Verwijder een wagentype uit de data source
        /// </summary>
        /// <param name="wagenType">wagentype dat moet verwijderd worden</param>
        public void VerwijderWagenType(WagenType wagenType)
        {
            if (_wagenTypeRepo.BestaatWagenType(wagenType))_wagenTypeRepo.VerwijderWagenType(wagenType.Id);
        }

        /// <summary>
        /// Update een wagentype uit de data source
        /// </summary>
        /// <param name="wagenType">het up te daten wagentype</param>
        public void UpdateWagenType(WagenType wagenType)
        {
            if (_wagenTypeRepo.BestaatWagenType(wagenType)) _wagenTypeRepo.UpdateWagenType(wagenType);
        }

        /// <summary>
        /// Geeft een overzicht van alle wagentypes
        /// </summary>
        /// <returns>een IEnumerable van Wagentypes</returns>
        public IEnumerable<WagenType> GeefAlleWagenTypes()
        {
            return _wagenTypeRepo.GeefAlleWagenTypes();
        }
    }
}