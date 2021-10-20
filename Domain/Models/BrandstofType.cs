using DomainLayer.Exceptions.Models;

namespace DomainLayer.Models
{
    public class BrandstofType
    {
        public int Id { get; private set; }
        public string Type { get; private set; }

        public BrandstofType(int id, string type) : this(type)
        {
            ZetId(id);
        }


        public BrandstofType(string type)
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
            this.Id = id;
        }


        /// <summary>
        /// Dit veranderd het type van de brandstof
        /// Controlleert of het niet null of leeg is.
        /// </summary>
        /// <param name="type"></param>
        public void ZetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new BrandstofTypeException("ZetType - Type is null of leeg");
            this.Type = type.Trim().ToUpper();
        }
    }

    
}