using DomainLayer.Models;
using System.Collections.Generic;

namespace DomainLayer.Interfaces.Repos
{
    public interface IWagenTypeRepo
    {
        void VoegWagenTypeToe(WagenType brandstofType);
        void VerwijderWagenType(WagenType brandstofType);
        void UpdateWagenType(WagenType brandstofType);
        IEnumerable<WagenType> GeefAlleWagenTypes();
        bool BestaatWagenType(WagenType brandstofType);
    }
}