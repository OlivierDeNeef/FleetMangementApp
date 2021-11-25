using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System.Collections.Generic;

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
            _rijbewijsTypeRepo.VoegRijbewijsToe(rijbewijsType);
        }

        /// <summary>
        /// Verwijder een rijbewijstype aan de data source.
        /// </summary>
        /// <param name="rijbewijsType">rijbewijstype dat moet worden verwijderd</param>
        public void VerwijderRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (_rijbewijsTypeRepo.BestaatRijbewijsType(rijbewijsType)) _rijbewijsTypeRepo.VerwijderRijbewijsType(rijbewijsType);
        }

        /// <summary>
        /// Update een rijbewijstype in de data source
        /// </summary>
        /// <param name="rijbewijsType">rijbewijstype dat moet worden geupdate</param>
        public void UpdateRijbewijsType(RijbewijsType rijbewijsType)
        {
            if (_rijbewijsTypeRepo.BestaatRijbewijsType(rijbewijsType)) _rijbewijsTypeRepo.UpdateRijbewijsType(rijbewijsType);
        }

        /// <summary>
        /// Geef alle rijbewijstypes terug
        /// </summary>
        /// <returns>een IEnumerable van RijbewijsType</returns>
        public IEnumerable<RijbewijsType> GeefAlleRijsbewijsTypes()
        {
            return _rijbewijsTypeRepo.GeefAlleRijbewijsTypes();
        }
    }
}