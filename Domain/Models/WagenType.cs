using System;
using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class WagenType
    {
        public int Id { get; private set; } // private maken!
        public string Type { get; private set; } //private maken!

        public WagenType(string type) //Todo : tests schrijven
        {
            ZetType(type);
        }
        public WagenType(int id, string type)
        {
            ZetId(id);
            ZetType(type);
        }
        /// <summary>
        /// zet het id van het wagentype
        /// </summary>
        /// <param name="id">WagentypeId</param>
        public void ZetId(int id)
        {
            if (id <= 0) throw new WagenTypeException("ZetId - id moet groter zijn dan 0");
            Id = id;
        }


        /// <summary>
        /// Zet het type van het wagentype
        /// </summary>
        /// <param name="type">Wagentype van de wagen</param>
        public void ZetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new WagenTypeException("ZetType - mag niet leeg zijn");
            if(type.Trim() == Type) throw new WagenTypeException("ZetType - zelfde type als huidig type");
            Type = type.Trim();
        }

        /// <summary>
        /// Controlleer twee wagentypes of deze hetzelfde zijn
        /// </summary>
        /// <param name="obj">Obeject om te vergelijken</param>
        /// <returns></returns>
        public override bool Equals(object obj) //Todo : tests schrijven
        {
            return obj is WagenType other && Id == other.Id && Type == other.Type;
        }

        /// <summary>
        /// Geeft de hashcode van wagentype
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }

      
    }
}