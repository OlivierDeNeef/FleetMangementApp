using DomainLayer.Models;
using FleetMangementApp.Models.Output;

namespace FleetMangementApp.Mappers
{
    public static class BestuurderUIMapper
    {
        public static ResultBestuurder ToUI(Bestuurder bestuurder)
        {
            return new ResultBestuurder() {Id = bestuurder.Id, Naam = bestuurder.Naam, Voornaam = bestuurder.Voornaam, Geboortedatum = bestuurder.Geboortedatum.ToShortDateString(), HeeftTankkaart = (bestuurder.Tankkaart != null), HeeftVoertuig = (bestuurder.Voertuig != null)};
        }
    }
}