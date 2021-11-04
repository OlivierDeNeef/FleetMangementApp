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
    public class VoertuigRepo: IVoertuigRepo
    {
        public void BestaatVoertuig(Voertuig voertuig)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Voertuig> GeefGefilterdeVoertuigen([Optional] int id,[Optional] string merk,[Optional] string model,[Optional] int aantalDeuren,[Optional] string nummerplaat,[Optional] string chassisnummer,[Optional] string kleur,[Optional] WagenType wagenType,[Optional] BrandstofType brandstofType,[Optional] bool geachriveerd,[Optional] RijbewijsType type)
        {
            throw new NotImplementedException();
        }

        public Voertuig GeefTVoertuig(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateVoertuig(Voertuig voertuig)
        {
            throw new NotImplementedException();
        }

        public void VoegWagenToe(Voertuig voertuig)
        {
            throw new NotImplementedException();
        }
    }
}
