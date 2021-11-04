using System.Collections.Generic;
using System.Runtime.InteropServices;
using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repos
{
    public interface IVoertuigRepo
    {
        /// <summary>
        /// Check nodig of minstens één van de velden is ingevuld (!= null) 0
        /// </summary>
        /// <param name="id"></param>
        /// <param name="merk"></param>
        /// <param name="model"></param>
        /// <param name="aantalDeuren"></param>
        /// <param name="nummerplaat"></param>
        /// <param name="chassisnummer"></param>
        /// <param name="kleur"></param>
        /// <param name="wagenType"></param>
        /// <param name="brandstofType"></param>
        /// <param name="geachriveerd"></param>
        /// <param name="type"></param>
        /// <returns>lijst met voertuigen die voldoen aan de filter</returns>
        public IReadOnlyList<Voertuig> GeefGefilterdeVoertuigen([Optional] int id, [Optional] string merk,
            [Optional] string model, [Optional] int aantalDeuren, [Optional] string nummerplaat,
            [Optional] string chassisnummer, [Optional] string kleur, [Optional] WagenType wagenType,
            [Optional] BrandstofType brandstofType, [Optional] bool geachriveerd, [Optional] RijbewijsType type);

        /// <summary> 
        /// Check of bestuurder het nodige rijbewijs heeft
        /// Check of Tankkaart kan gebruikt worden bij het voertuig
        /// </summary>
        /// <param name="voertuig"></param>
        public void VoegVoertuigToe(Voertuig voertuig);
        public void UpdateVoertuig(Voertuig voertuig);
        public bool BestaatVoertuig(Voertuig voertuig);
        public Voertuig GeefVoertuig(int id);


    }
}