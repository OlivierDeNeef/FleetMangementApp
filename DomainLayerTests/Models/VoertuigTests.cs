using System;
using System.Collections.Generic;
using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class VoertuigTests
    {
        private readonly Voertuig _voertuig;

      
        public VoertuigTests()
        {
            _voertuig = new Voertuig("Bmw", "X5", "wauzzz8v5ka106598", "1-abc-123", new BrandstofType("diezel"), new WagenType("Auto"));
        } 
        [Fact()]
        public void ZetIdTest()
        {
            Assert.Equal(0,_voertuig.Id);

            _voertuig.ZetId(4);

            Assert.Equal(4, _voertuig.Id);

        }

        [Fact()]
        public void ZetId_NegatieveId_GooitException()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetId(0));
        }

        [Fact()]
        public void ZetMerkTestGeldig()
        {
            Assert.Equal("Bmw", _voertuig.Merk);

            _voertuig.ZetMerk("Mercedez");

            Assert.Equal("Mercedez", _voertuig.Merk);
        }

         [Theory]
         [InlineData("  ")]
         [InlineData(null)]
        public void ZetMerkOngeldig(string merk)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetMerk(merk));
        }

        [Fact()]
        public void ZetModelGeldig()
        {
            Assert.Equal("X5", _voertuig.Model);

            _voertuig.ZetModel("A-Klasse");

            Assert.Equal("A-Klasse", _voertuig.Model);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData(null)]
        public void ZetModelOngeldig(string model)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetModel(model));
        }

        [Fact()]
        public void ZetChassisnummerTest()
        {
            Assert.Equal("wauzzz8v5ka106598", _voertuig.Chassisnummer);

            _voertuig.ZetChassisnummer("123456ABCDEF789GH");

            Assert.Equal("123456ABCDEF789GH", _voertuig.Chassisnummer);
        }

        [Theory]
        [InlineData("123456ABCDEF78")]
        [InlineData("")]
        [InlineData(null)]
        public void ZetChassisnummerInValid(string nummer)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetChassisnummer(nummer));
        }

        [Fact()]
        public void ZetWagenTypeTest()
        {
            Assert.Equal(new WagenType("Auto"), _voertuig.WagenType);
            var w = new WagenType("Bestelbus");

            _voertuig.ZetWagenType(w);

            Assert.Equal(w, _voertuig.WagenType );
        }

        [Fact()]
        public void ZetWagenTypeNullTest()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetWagenType(null));
        }

        [Fact]
        public void ZetWagenTypeIdInvalidAndInvalidType()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetWagenType(null));
        }
        
        [Fact()]
        public void ZetBrandstofTypeNull()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetBrandstofType(null));
        }

        [Fact()]
        public void ZetBrandstofTypeTest()
        {
            var brandstofType = new BrandstofType("Benzine");

            Assert.Equal(new BrandstofType("diezel"), _voertuig.BrandstofType);

            _voertuig.ZetBrandstofType(brandstofType);

            Assert.Equal(brandstofType, _voertuig.BrandstofType);
            
        }

        [Theory]
        [InlineData("1-123-DEF")]// Ask : ongeldige formaat toch
        [InlineData("123-ABC")]
        public void ZetNummerplaatTest(string plaat)
        {
            Assert.Equal("1-abc-123",_voertuig.Nummerplaat);

            _voertuig.ZetNummerplaat(plaat);

            Assert.Equal(plaat, _voertuig.Nummerplaat);
        }

        [Theory]
        [InlineData("12-CD")]
        [InlineData("ABC-123-195")]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void ZetNummerplaatInvalid(string plaat)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetNummerplaat(plaat));
        }
        [Fact()]
        public void ZetKleurTest()
        {
            Assert.Null(_voertuig.Kleur);

            _voertuig.ZetKleur("Rood");

            Assert.Equal("Rood", _voertuig.Kleur);

        }

        [Fact()]
        public void ZetAantalDeurenTest()
        {
            Assert.Equal(0,_voertuig.AantalDeuren);

            _voertuig.ZetAantalDeuren(4);

            Assert.Equal(4, _voertuig.AantalDeuren);

        }

        [Theory]
        [InlineData(2)]
        [InlineData(7)]
        public void ZetAantalDeurenOngeldig(int aantal)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetAantalDeuren(aantal));
        }

        [Fact()]
        public void ZetBestuurderTestGeldig() 
        {
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };
            var bestuurder1 = new Bestuurder("De Nef", "Olivier", new DateTime(1999, 10, 6), "99100630515", rijbewijzen);
            var bestuurder2 = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",rijbewijzen);
            
            bestuurder2.ZetVoertuig(_voertuig);
            _voertuig.ZetBestuurder(bestuurder1);

            Assert.Equal(bestuurder1, _voertuig.Bestuurder);
            Assert.Equal(_voertuig, bestuurder1.Voertuig);

            _voertuig.ZetBestuurder(bestuurder2);

            Assert.Equal(bestuurder2, _voertuig.Bestuurder);
            Assert.Equal(_voertuig, bestuurder2.Voertuig);
            Assert.Null(bestuurder1.Voertuig);
        }

        [Fact()]
        
        public void ZetBestuurderTestOngeldig()
        {
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", rijbewijzen);
            _voertuig.ZetBestuurder(bestuurder);

            Assert.Equal(bestuurder, _voertuig.Bestuurder);
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetBestuurder(bestuurder));
        }



        [Fact()]
        public void VerwijderBestuurder()
        {
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", rijbewijzen);
            _voertuig.ZetBestuurder(bestuurder);

            Assert.Equal(bestuurder,_voertuig.Bestuurder);
            Assert.Equal(_voertuig,bestuurder.Voertuig);

            _voertuig.VerwijderBestuurder();

            Assert.Null(_voertuig.Bestuurder);
            Assert.Null(bestuurder.Voertuig);
        }


        [Fact()]
        public void VerwijderBestuurderIsNull()
        {
            Assert.Null(_voertuig.Bestuurder);
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.VerwijderBestuurder());
        }

        [Fact()]
        public void ZetIsGearchiveerdTrue()
        {
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",
                rijbewijzen);
            _voertuig.ZetBestuurder(bestuurder);

            Assert.Equal(bestuurder, _voertuig.Bestuurder);
            Assert.Equal(_voertuig, bestuurder.Voertuig);

            _voertuig.ZetGearchiveerd(true);

            Assert.True(_voertuig.IsGearchiveerd);
            Assert.Null(_voertuig.Bestuurder);
            Assert.Null(bestuurder.Voertuig);
        }
        [Fact()]
        public void ZetIsGearchiveerdFalse()
        {
            _voertuig.ZetGearchiveerd(true);

            Assert.True(_voertuig.IsGearchiveerd);

            _voertuig.ZetGearchiveerd(false);

            Assert.False(_voertuig.IsGearchiveerd);
        }
    }
}