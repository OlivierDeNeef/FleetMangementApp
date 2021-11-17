﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface ITankkaartRepo
    {
        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten([Optional] int id, [Optional] string kaartnummer,
            [Optional] DateTime geldigheidsdatum, [Optional] List<BrandstofType> lijstBrandstoftypes,
            [Optional] bool geachiveerd);
        public void VoegTankkaartToe(Tankkaart t);
        public void UpdateTankkaart(Tankkaart t);
        public bool BestaatTankkaart(int tankkaartId);

        public Tankkaart GeefTankkaart(int id);


    }
}