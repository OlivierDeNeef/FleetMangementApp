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
    public static class TankkaartUIMapper
    {
        public static ResultTankkaart ToUI(Tankkaart tankkaart)
        {
            return new ResultTankkaart
            {
                Id = tankkaart.Id,
                Kaartnummer = tankkaart.Kaartnummer,
                Pincode = tankkaart.Pincode,
                Geldigheidsdatum = tankkaart.Geldigheidsdatum.ToShortDateString(),
                IsGeblokkeerd = tankkaart.IsGeblokkeerd,
                HeeftBestuurder = tankkaart.Bestuurder != null
            };
        }

        public static Tankkaart FromUI(ResultTankkaart tankkaart, TankkaartManager repo)
        {
            return repo.GeefTankkaart(tankkaart.Id);
        }
    }
}
