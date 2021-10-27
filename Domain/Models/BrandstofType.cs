using System;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class BrandstofType
    {
        public int Id { get; private set; }
        public string Type { get; private set; }

        public BrandstofType(int id, string type) : this(type)//Todo: tests schrijven
        {
            ZetId(id);
        }


        public BrandstofType(string type)//Todo: tests schrijven
        {
            ZetType(type);
        }

        /// <summary>
        /// Dit veranderd de id van het brandstofType
        /// controlleer of het id groter is dan 1
        /// </summary>
        /// <param name="id"></param>
        public void ZetId(int id)
        {
            if (id < 1) throw new BrandstofTypeException("ZetId - id < 1");
            Id = id;
        }


        /// <summary>
        /// Dit veranderd het type van de brandstof
        /// Controlleert of het niet null of leeg is.
        /// </summary>
        /// <param name="type"></param>
        public void ZetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new BrandstofTypeException("ZetType - Type is null of leeg");
            if (type.Trim().ToUpper() == Type) throw new BrandstofTypeException("ZetType - zelfde type als huidig type");
            Type = type.Trim().ToUpper();
        }
        /// <summary>
        /// Controlleert of 2 brandstoftypes hetzelfde zijn 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)//Todo: tests schrijven
        {
            return obj is BrandstofType other && Id == other.Id && Type == other.Type;
        }

      
        /// <summary>
        /// Geeft de hashcode van een brandstoftype
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }
    }

    
}