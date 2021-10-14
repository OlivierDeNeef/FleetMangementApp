using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    /// <summary>
    /// Todo : SetNummerplaat uitgebreider testen
    /// Todo : SetBestuurder uitgebreider testen
    /// Todo : Tests schrijven voor verwijder bestuurder
    /// Todo : Ook testen op null waarde
    /// </summary>


    public class VoertuigTests
    {
        private readonly Voertuig _voertuig;

      
        public VoertuigTests()
        {
            _voertuig = new Voertuig();
        } 
        [Fact()]
        public void SetIdTest()
        {
            _voertuig.SetId(4);
            Assert.Equal(4, _voertuig.Id);

        }

        [Fact()]
        public void SetId_NegatieveId_ThrowException()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.SetId(-4));
        }

        [Fact()]
        public void SetMerkTest()
        {
           _voertuig.SetMerk("Mercedez");
           Assert.Equal("Mercedez", _voertuig.Merk);
        }

        [Fact()]
        public void SetMerkIsNull()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.SetMerk(""));
        }

        [Fact()]
        public void SetModelTest()
        {
            _voertuig.ZedModel("A-Klasse");
            Assert.Equal("A-Klasse", _voertuig.Model);
        }

        [Fact()]
        public void SetModelIsNull()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZedModel(""));
        }

        [Fact()]
        public void SetChassisnummerTest()
        {
            _voertuig.ZetChassisnummer("123456ABCDEF789GH");
            Assert.Equal("123456ABCDEF789GH", _voertuig.Chassisnummer);
        }

        [Theory]
        [InlineData("123456ABCDEF78")]
        [InlineData("")]
        public void SetChassisnummerInValid(string nummer)
        {
            
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetChassisnummer(nummer));
        }

        [Fact()]
        public void SetWagenTypeTest()
        {
            WagenType w = new WagenType();
            w.Id = 1;
            w.Type = "Bestelbus";
            _voertuig.SetWagenType(w);

            Assert.Equal(1, _voertuig.WagenType.Id);
            Assert.Equal("Bestelbus", _voertuig.WagenType.Type);
        }

        [Fact()]
        public void SetWagenTypeNullTest()
        {
            WagenType w = null;
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.SetWagenType(w));

        }

        [Theory]
        [InlineData(0, "Bestelbus")]
        [InlineData(1, "")]
        public void SetWagenTypeIdInvalidAndInvalidType(int id, string type)
        {
            WagenType w = new WagenType();
            w.Id = id;
            w.Type = type;
           // _voertuig.SetWagenType(w);

            Assert.ThrowsAny<VoertuigException>(() => _voertuig.SetWagenType(w));

        }
        
        [Fact()]
        public void SetBrandstofTypeNull()
        {
            BrandstofType b = null;
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.SetBrandstofType(b));
        }
        [Fact()]
        public void SetBrandstofTypeTest()
        {
            BrandstofType b = new BrandstofType();
            b.ZetId(1);
            b.ZetType("Benzine");
           _voertuig.SetBrandstofType(b);

            Assert.Equal(1, _voertuig.BrandstofType.Id);
            Assert.Equal("Benzine", _voertuig.BrandstofType.Type);
        }

        [Theory]
        [InlineData("1-123-DEF")]
        [InlineData("123-ABC")]
        public void SetNummerplaatTest(string plaat)
        {
            _voertuig.SetNummerplaat(plaat);
            Assert.Equal(plaat, _voertuig.Nummerplaat);
        }

        [Theory]
        [InlineData("12-CD")]
        [InlineData("")]
        public void SetNummerplaatInvalid(string plaat)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.SetNummerplaat(plaat));
        }
        [Fact()]
        public void SetKleurTest()
        {
            _voertuig.SetKleur("Rood");
            Assert.Equal("Rood", _voertuig.Kleur);

        }

        [Fact()]
        public void SetAantalDeurenTest()
        {
            _voertuig.SetAantalDeuren(4);
            Assert.Equal(4, _voertuig.AantalDeuren);

        }

        [Theory]
        [InlineData(2)]
        [InlineData(7)]
        public void SetAantalDeurenInvalid(int aantal)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.SetAantalDeuren(aantal));
        }
        [Fact()]
        public void SetBestuurderTest() // TODO: Olivier
        {
            Assert.True(true, "This test needs an implementation");
        }

        [Fact()]
        public void SetIsDeletedTestTrue()
        {
            _voertuig.SetIsDeleted(true);
            Assert.True(_voertuig.IsDeleted);
        }
        [Fact()]
        public void SetIsDeletedTestFalse()
        {
            _voertuig.SetIsDeleted(false);
            Assert.False(_voertuig.IsDeleted);
        }
    }
}