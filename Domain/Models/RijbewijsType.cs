using System;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class RijbewijsType
    {
        public int Id { get; private set; }
        public string Type { get; private set; }


        public RijbewijsType(string type)//Todo: tests schrijven
        {
            ZetType(type);
        }

        public RijbewijsType(int id, string type) : this(type)//Todo: tests schrijven
        {
            ZetId(id);
        }
        /// <summary>
        /// Dit veranderd de id van het rijbewijstype
        /// controlleer of het id groter is dan 1
        /// </summary>
        /// <param name="id"></param>
        public void ZetId(int id)
        {
            if (id < 1)
                throw new RijbewijsTypeException("ZetId - id < 1");
            Id = id;
        }
        /// <summary>
        /// Dit veranderd het type van het rijbewijs
        /// Controlleert of het type niet null of leeg is
        /// </summary>
        /// <param name="type"></param>
        public void ZetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new RijbewijsTypeException("Zet Type - Type is null of leeg");
            if(type.Trim() == Type) throw new RijbewijsTypeException("ZetType - type mag niet hetzelfde zijn als huidig type");
            Type = type.Trim();
        }

        /// <summary>
        /// Controlleert of 2 wagen types het zelfde zijn 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)//Todo: tests schrijven
        {
            return obj is RijbewijsType other && Id == other.Id && Type == other.Type;
        }

        /// <summary>
        /// Geeft de hash code van een wagen type
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }
    }
}