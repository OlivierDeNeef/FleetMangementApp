using System.Collections;
using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface IRijbewijsTypeRepo
    {
        void VoegRijbewijsToe(RijbewijsType rijbewijsType);
        void VerwijderRijbewijsType(RijbewijsType rijbewijsType);

        IEnumerable<RijbewijsType> GeefAlleRijbewijsTypes();
        bool BestaatRijbewijsType(RijbewijsType rijbewijsType);
        void UpdateRijbewijsType(RijbewijsType rijbewijsType);



    }
}