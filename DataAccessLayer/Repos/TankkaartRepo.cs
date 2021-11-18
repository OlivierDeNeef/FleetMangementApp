using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class TankkaartRepo: ITankkaartRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public TankkaartRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public bool BestaatTankkaart(Tankkaart t)
        {
            throw new NotImplementedException();
        }

        public bool BestaatTankkaart(int tankkaartId)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten([Optional] int id,[Optional] string kaartnummer,[Optional] DateTime geldigheidsdatum,[Optional] List<BrandstofType> lijstBrandstoftypes,[Optional] bool geachiveerd)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten([Optional] string kaartnummer, [Optional] DateTime geldigheidsdatum, [Optional] List<BrandstofType> lijstBrandstoftypes, [Optional] bool geachiveerd)
        {
            throw new NotImplementedException();
        }

        public Tankkaart GeefTankkaart(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTankkaart(Tankkaart t)
        {
            throw new NotImplementedException();
        }

        public void VoegTankkaartToe(Tankkaart t)
        {
            throw new NotImplementedException();
        }
    }
}
