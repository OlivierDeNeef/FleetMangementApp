using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    /// <summary>
    /// Todo : ZetNummerplaat uitgebreider testen
    /// Todo : ZetBestuurder uitgebreider testen
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
            _voertuig.ZetId(4);
            Assert.Equal(4, _voertuig.Id);

        }

        [Fact()]
        public void SetId_NegatieveId_ThrowException()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetId(-4));
        }

        [Fact()]
        public void SetMerkTest()
        {
           _voertuig.ZetMerk("Mercedez");
           Assert.Equal("Mercedez", _voertuig.Merk);
        }

        [Fact()]
        public void SetMerkIsNull()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetMerk(""));
        }

        [Fact()]
        public void SetModelTest()
        {
            _voertuig.ZetModel("A-Klasse");
            Assert.Equal("A-Klasse", _voertuig.Model);
        }

        [Fact()]
        public void SetModelIsNull()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetModel(""));
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
            _voertuig.ZetWagenType(w);

            Assert.Equal(1, _voertuig.WagenType.Id);
            Assert.Equal("Bestelbus", _voertuig.WagenType.Type);
        }

        [Fact()]
        public void SetWagenTypeNullTest()
        {
            WagenType w = null;
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetWagenType(w));

        }

        [Theory]
        [InlineData(0, "Bestelbus")]
        [InlineData(1, "")]
        public void SetWagenTypeIdInvalidAndInvalidType(int id, string type)
        {
            WagenType w = new WagenType();
            w.Id = id;
            w.Type = type;
           // _voertuig.ZetWagenType(w);

            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetWagenType(w));

        }
        
        [Fact()]
        public void SetBrandstofTypeNull()
        {
            BrandstofType b = null;
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetBrandstofType(b));
        }
        [Fact()]
        public void SetBrandstofTypeTest()
        {
            BrandstofType b = new BrandstofType();
            b.Id = 1;
            b.Type = "Benzine";
           _voertuig.ZetBrandstofType(b);

            Assert.Equal(1, _voertuig.BrandstofType.Id);
            Assert.Equal("Benzine", _voertuig.BrandstofType.Type);
        }

        [Theory]
        [InlineData("1-123-DEF")]
        [InlineData("123-ABC")]
        public void SetNummerplaatTest(string plaat)
        {
            _voertuig.ZetNummerplaat(plaat);
            Assert.Equal(plaat, _voertuig.Nummerplaat);
        }

        [Theory]
        [InlineData("12-CD")]
        [InlineData("")]
        public void SetNummerplaatInvalid(string plaat)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetNummerplaat(plaat));
        }
        [Fact()]
        public void SetKleurTest()
        {
            _voertuig.ZetKleur("Rood");
            Assert.Equal("Rood", _voertuig.Kleur);

        }

        [Fact()]
        public void SetAantalDeurenTest()
        {
            _voertuig.ZetAantalDeuren(4);
            Assert.Equal(4, _voertuig.AantalDeuren);

        }

        [Theory]
        [InlineData(2)]
        [InlineData(7)]
        public void SetAantalDeurenInvalid(int aantal)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetAantalDeuren(aantal));
        }
        [Fact()]
        public void SetBestuurderTest() // TODO: Olivier
        {
            Assert.True(true, "This test needs an implementation");
        }

        [Fact()]
        public void SetIsDeletedTestTrue()
        {
            _voertuig.ZetGearchiveerd(true);
            Assert.True(_voertuig.IsGearchiveerd);
        }
        [Fact()]
        public void SetIsDeletedTestFalse()
        {
            _voertuig.ZetGearchiveerd(false);
            Assert.False(_voertuig.IsGearchiveerd);
        }
    }
}