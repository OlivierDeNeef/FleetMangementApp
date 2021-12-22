
using DomainLayer.Managers;
using DomainLayer.Models;
using FleetMangementApp.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMangementApp.Mappers
{
    public static class VoertuigUIMapper
    {
        public static ResultVoertuig ToUI(Voertuig voertuig)
        {
            return new ResultVoertuig()
            {
                Id = voertuig.Id,
                Merk = voertuig.Merk,
                Model = voertuig.Model,
                Nummerplaat = voertuig.Nummerplaat,
                Chassisnummer = voertuig.Chassisnummer,
                WagenType = voertuig.WagenType.Type,
                Brandstof = voertuig.BrandstofType.Type,
                HeeftBestuurder = voertuig.Bestuurder != null
            };
        }

        public static Voertuig FromUI(ResultVoertuig resultVoertuig, VoertuigManager repo)
        {
            return repo.GeefVoertuig(resultVoertuig.Id);
        }
    }
}
