using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface IVoertuigRepo
    {
        bool BestaatVoertuig(Voertuig voertuig);
        IReadOnlyList<Voertuig> GeefGefilterdeVoertuigen(string merk, string model, int aantalDeuren, string nummerplaat, string chassisnummer, string kleur, WagenType wagenType, BrandstofType brandstofType, bool gearchiveerd, bool isHybride);
        Voertuig GeefVoertuig(int id);
        void UpdateVoertuig(Voertuig voertuig);
        void VoegVoertuigToe(Voertuig voertuig);
    }
}