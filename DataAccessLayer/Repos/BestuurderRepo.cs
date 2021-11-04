using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class BestuurderRepo: IBestuurderRepo
    {
        public bool BestaatBestuurder(Bestuurder b)
        {
            throw new NotImplementedException();
        }

        public Bestuurder GeefBestuurder(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Bestuurder> GeefGefilterdeBestuurders([Optional] int id,[Optional] string voornaam,[Optional] string naam,[Optional] DateTime geboortedatum,[Optional] List<RijbewijsType> lijstRijbewijstypes,[Optional] string rijksregisternummer,[Optional] bool gearchiveerd)
        {
            throw new NotImplementedException();
        }

        public void UpdateBestuurder(Bestuurder b)
        {
            throw new NotImplementedException();
        }

        public void VoegBestuurderToe(Bestuurder b)
        {
            throw new NotImplementedException();
        }
    }
}
