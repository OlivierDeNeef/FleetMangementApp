using System;
using System.Collections.Generic;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface ITankkaartRepo
    {
        bool BestaatTankkaart(int tankkaartId);
        Tankkaart GeefTankkaart(int id);
        void VoegTankkaartToe(Tankkaart tankkaart);
        void UpdateTankkaart(Tankkaart tankkaart);
        IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten(string kaartnummer, DateTime geldigheidsdatum, List<BrandstofType> lijstBrandstoftypes, bool geachiveerd);
    }
}