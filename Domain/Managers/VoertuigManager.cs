using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces;
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
            try
            {
                if (!_voertuigRepo.BestaatVoertuig(voertuig))
                {
                    _voertuigRepo.VoegVoertuigToe(voertuig);
                }
            }
            catch
            {
                throw new VoertuigManagerException("VoegWagenToe - Er ging iets mis bij het toevoegen");
            }
        }
        public void UpdateVoertuig(Voertuig voertuig)
        {
            try
            {
                if (_voertuigRepo.BestaatVoertuig(voertuig))
                {
                    _voertuigRepo.UpdateVoertuig(voertuig);
                }
            }
            catch
            {
                throw new VoertuigManagerException("UpdateVoertuig - Er ging iets mis bij het updaten");

            }
        }
        public Voertuig GeefVoertuig(int id)
        {
            try
            {
                return _voertuigRepo.GeefVoertuig(id);
            }
            catch
            {
                throw new VoertuigManagerException("GeefVoertuig - Er ging iets mis");

            }
        }

        public IReadOnlyList<Voertuig> GeefGefilterdeVoertuigen([Optional] int id, [Optional] string merk,
            [Optional] string model, [Optional] int aantalDeuren, [Optional] string nummerplaat,
            [Optional] string chassisnummer, [Optional] string kleur, [Optional] WagenType wagenType,
            [Optional] BrandstofType brandstofType, [Optional] bool gearchiveerd, [Optional] RijbewijsType type)
        {
            var lijstVoertuigen = new List<Voertuig>();
            try
            {
                if (id > 0)
                {
                    lijstVoertuigen.Add(_voertuigRepo.GeefVoertuig(id));
                    return lijstVoertuigen;
                }
                return _voertuigRepo.GeefGefilterdeVoertuigen(id, merk, model, aantalDeuren, nummerplaat, chassisnummer, kleur, wagenType, brandstofType, gearchiveerd, type);

            }
            catch (Exception e)
            {
                throw new VoertuigManagerException("GeefGefilterdeVoertuigen, er ging iets mis", e);
            }
        }


    }
}