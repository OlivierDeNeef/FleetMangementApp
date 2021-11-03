using DomainLayer.Models;
using System.Collections.Generic;

namespace DomainLayer.Interfaces.Repos
{
    public interface IWagenTypeRepo
    {
        void VoegWagenTypeToe(WagenType brandstofType);
        void VerwijderWagenType(int id);
        void UpdateWagenType(WagenType wagenType);
        IEnumerable<WagenType> GeefAlleWagenTypes();
        bool BestaatWagenType(WagenType brandstofType);
    }
}