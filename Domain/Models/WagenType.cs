using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class WagenType
    {
        public int Id { get; set; } // private maken!
        public string Type { get; set; } //private maken!

        public WagenType(string type)
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
            Type = type.Trim();
        }

    }
}