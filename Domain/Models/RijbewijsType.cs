using DomainLayer.Exceptions.Managers;

namespace DomainLayer.Models
{
    public class RijbewijsType
    {
        public int Id { get; private set; }
        public string Type { get; private set; }


        public RijbewijsType(string type)
        {
            ZetType(type);
        }

        public RijbewijsType(int id, string type) : this(type)
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
                throw new RijbewijsTypeManagerException("ZetId - id < 1");
            Id = id;
        }
        /// <summary>
        /// Dit veranderd het type van het rijbewijs
        /// Controlleert of het type niet null of leeg is
        /// </summary>
        /// <param name="type"></param>
        public void ZetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new RijbewijsTypeManagerException("Zet Type - Type is null of leeg");
            Type = type.Trim();
        }
        
    }
}