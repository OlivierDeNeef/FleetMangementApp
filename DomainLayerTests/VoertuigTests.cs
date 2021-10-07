using Xunit;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;

namespace DomainLayer.Tests
{
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
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetId(-4));
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
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetMerk(""));
        }

        [Fact()]
        public void SetModelTest()
        {
            _voertuig.SetModel("A-Klasse");
            Assert.Equal("A-Klasse", _voertuig.Model);
        }

        [Fact()]
        public void SetModelIsNull()
        {
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetModel(""));
        }

        [Fact()]
        public void SetChassisnummerTest()
        {
            _voertuig.SetChassisnummer("123456ABCDEF789GH");
            Assert.Equal("123456ABCDEF789GH", _voertuig.Chassisnummer);
        }

        [Theory]
        [InlineData("123456ABCDEF78")]
        [InlineData("")]
        public void SetChassisnummerInValid(string nummer)
        {
            
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetChassisnummer(nummer));
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
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetWagenType(w));

        }

        [Theory]
        [InlineData(0, "Bestelbus")]
        [InlineData(1, "")]
        public void SetWagenTypeIdInvalid(int id, string type)
        {
            WagenType w = new WagenType();
            w.Id = id;
            w.Type = type;
           // _voertuig.SetWagenType(w);

            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetWagenType(w));

        }
        
        [Fact()]
        public void SetBrandstofTypeNull()
        {
            BrandstofType b = null;
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetBrandstofType(b));
        }
        [Fact()]
        public void SetBrandstofTypeTest()
        {
            BrandstofType b = new BrandstofType();
            b.Id = 1;
            b.Type = "Benzine";
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
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetNummerplaat(plaat));
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
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetAantalDeuren(aantal));
        }
        [Fact()]
        public void SetBestuurderTest() // TODO Olivier
        {
            Assert.True(false, "This test needs an implementation");
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