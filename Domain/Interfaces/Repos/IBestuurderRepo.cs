using System;
using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface IBestuurderRepo
    {
        IReadOnlyList<Bestuurder> GeefGefilderdeBestuurders(string voornaam, string naam, DateTime geboortedatum, List<RijbewijsType> lijstRijbewijstypes, string rijksregisternummer, bool gearchiveerd);
        void VoegBestuurderToe(Bestuurder bestuurder);
        bool BestaatBestuurder(int bestuurderId);
        void VerwijderBestuurder(int id);
        void UpdateBestuurder(Bestuurder bestuurder);
        Bestuurder GeefBestuurder(int id);
    }
}