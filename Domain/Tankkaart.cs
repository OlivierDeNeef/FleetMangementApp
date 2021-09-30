using System;
using System.Collections.Generic;

namespace DomainLayer
{
    public class Tankkaart
    {
        public int Id { get; set; }
        public string Kaartnummer { get; set; }
        public DateTime Geldigheidsdatum { get; set; }
        public int Pincode { get; set; }
        private readonly List<BrandstofType> _brandstofTypes = new();
        public Bestuurder Bestuurder { get; set; }
        public bool IsDeleted { get; set; }
    }
}