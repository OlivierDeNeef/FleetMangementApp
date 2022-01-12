using System;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System.Collections.Generic;
using DomainLayer.Exceptions.Managers;

namespace DomainLayer.Managers
{
    public class RijbewijsTypeManager
    {
        private readonly IRijbewijsTypeRepo _rijbewijsTypeRepo;

        public RijbewijsTypeManager(IRijbewijsTypeRepo rijbewijsTypeRepo)
        {
            _rijbewijsTypeRepo = rijbewijsTypeRepo;
        }
        /// <summary>
        /// Voeg een rijbewijstype toe aan de data source.
        /// </summary>
        /// <param name="rijbewijsType">rijbewijstype dat moet worden toegevoegd</param>
        public void VoegRijbewijsTypeToe(RijbewijsType rijbewijsType)
        {
            try
            {
                _rijbewijsTypeRepo.VoegRijbewijsToe(rijbewijsType);
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("Er ging iets mis", e);
            }
        }

        /// <summary>
        /// Verwijder een rijbewijstype aan de data source.
        /// </summary>
        /// <param name="rijbewijsType">rijbewijstype dat moet worden verwijderd</param>
        public void VerwijderRijbewijsType(RijbewijsType rijbewijsType)
        {
            try
            {
                if (_rijbewijsTypeRepo.BestaatRijbewijsType(rijbewijsType)) _rijbewijsTypeRepo.VerwijderRijbewijsType(rijbewijsType);
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("Er ging iets mis", e);
            }
        }

        /// <summary>
        /// Update een rijbewijstype in de data source
        /// </summary>
        /// <param name="rijbewijsType">rijbewijstype dat moet worden geupdate</param>
        public void UpdateRijbewijsType(RijbewijsType rijbewijsType)
        {

            try
            {
                if (_rijbewijsTypeRepo.BestaatRijbewijsType(rijbewijsType)) _rijbewijsTypeRepo.UpdateRijbewijsType(rijbewijsType);
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("Er ging iets mis", e);
            }
        }

        /// <summary>
        /// Geef alle rijbewijstypes terug
        /// </summary>
        /// <returns>een IEnumerable van RijbewijsType</returns>
        public IEnumerable<RijbewijsType> GeefAlleRijsbewijsTypes()
        {
            try
            {
                return _rijbewijsTypeRepo.GeefAlleRijbewijsTypes();
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeManagerException("Er ging iets mis", e);
            }
        }
    }
}