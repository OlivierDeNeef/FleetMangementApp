using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DomainLayer.Managers
{
    public class VoertuigManager
    {
        private readonly IVoertuigRepo _voertuigRepo;

        public VoertuigManager(IVoertuigRepo voertuigRepo)
        {
            _voertuigRepo = voertuigRepo;
        }

        public void VoegVoertuigToe(Voertuig voertuig)
        {
             _voertuigRepo.VoegVoertuigToe(voertuig);
        }

        public void UpdateVoertuig(Voertuig voertuig)
        {
            if (_voertuigRepo.BestaatVoertuig(voertuig)) _voertuigRepo.UpdateVoertuig(voertuig);
        }

        public Voertuig GeefVoertuig(int id)
        {
            return _voertuigRepo.GeefVoertuig(id);
        }

        public IReadOnlyList<Voertuig> GeefGefilterdeVoertuigen( int id,  string merk, string model,  int aantalDeuren, string nummerplaat, string chassisnummer,  string kleur,  WagenType wagenType, BrandstofType brandstofType, bool gearchiveerd,  bool isHybride)
        {
            try
            {
                var lijstVoertuigen = new List<Voertuig>();
                if (id <= 0) return _voertuigRepo.GeefGefilterdeVoertuigen(merk, model, aantalDeuren, nummerplaat, chassisnummer, kleur, wagenType, brandstofType, gearchiveerd, isHybride);
                lijstVoertuigen.Add(_voertuigRepo.GeefVoertuig(id));
                return lijstVoertuigen;
            }
            catch (Exception e)
            {
                throw new VoertuigManagerException(nameof(GeefGefilterdeVoertuigen) + " Er ging iets mis", e);
            }
        }


    }
}