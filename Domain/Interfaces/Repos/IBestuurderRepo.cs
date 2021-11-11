using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface IBestuurderRepo
    {


        public IReadOnlyList<Bestuurder> GeefGefilderdeBestuurders(string voornaam, string naam,
             DateTime geboortedatum, List<RijbewijsType> lijstRijbewijstypes,  string rijksregisternummer,bool gearchiveerd);

        /// <summary>
        /// check of voertuig hoort bij Rijbewijs van bestuurder
        /// Check of Tankkaart kan gebruikt worden bij het voertuig
        /// check ofdat rijksregisternummer bestaat
        /// </summary>
        /// <param name="b"></param>
        public void VoegBestuurderToe(Bestuurder b);

        /// <summary>
        /// Is er iets aangepast in de velden?
        /// check of voertuig hoort bij Rijbewijs van bestuurder
        /// Check of Tankkaart kan gebruikt worden bij het voertuig
        /// check ofdat rijksregisternummer al bestaat
        /// </summary>
        /// <param name="b"></param>
        public void UpdateBestuurder(Bestuurder b);


        /// <summary>
        /// Bestaat de bestuurder
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool BestaatBestuurder(int bestuurderId);
        
        /// <summary>
        /// Geeft een bestuurder op id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bestuurder GeefBestuurder(int id);

        /// <summary>
        /// verwijderd een bestuurder
        /// </summary>
        /// <param name="id"></param>
        public void VerwijderBestuurder(int id);
    }
}