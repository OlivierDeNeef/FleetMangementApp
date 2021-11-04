using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;

namespace DomainLayer.Managers
{
    public class VoertuigManager
    {
        private readonly IVoertuigRepo _voertuigRepo;

        public VoertuigManager(IVoertuigRepo voertuigRepo)
        {
            _voertuigRepo = voertuigRepo;
        }

        public void VoegWagenToe(Voertuig voertuig)
        {
            try
            {
                if(!_voertuigRepo.BestaatVoertuig(voertuig))
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
                if(_voertuigRepo.BestaatVoertuig(voertuig))
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
    }
}