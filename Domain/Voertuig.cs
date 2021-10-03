namespace DomainLayer
{
    public class Voertuig
    {
        public int Id { get; set; }
        public string Merk { get; set; }
        public string Model { get; set; }
        public string Chassisnummer { get; set; }
        public WagenType WagenType { get; set; }
        public BrandstofType BrandstofType { get; set; }
        public string Nummerplaat { get; set; }
        public string Kleur { get; set; }
        public int AantalDeuren { get; set; }
        public Bestuurder Bestuurder { get; set; }
        public bool IsDeleted { get; set; }
    }
}