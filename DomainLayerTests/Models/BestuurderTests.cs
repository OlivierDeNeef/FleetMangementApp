using System;
using System.Collections.Generic;
using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class BestuurderTests
    {
        private readonly Bestuurder _bestuurder;
        
        /// <summary>
        /// Setup voor elke test
        /// </summary>
        public BestuurderTests()
        {
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };
            _bestuurder =  new Bestuurder("De Neef", "Olivier", new DateTime(1999,10,6), "99100630515", rijbewijzen);
        }


        [Fact]
        public void BestuurderTest1_GeldigeCtor_PropertiesVeranderen()
        {
            var geboortedatum = new DateTime(1999, 10, 6);
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };

            var bestuurder = new Bestuurder("De Neef", "Olivier",geboortedatum , "99100630515",rijbewijzen );

            Assert.Equal("De Neef", bestuurder.Naam);
            Assert.Equal("Olivier", bestuurder.Voornaam);
            Assert.Equal(geboortedatum, bestuurder.Geboortedatum);
            Assert.Equal("99100630515", bestuurder.Rijksregisternummer);
            Assert.Equal(rijbewijzen,bestuurder.GeefRijbewijsTypes());
        }
        [Fact]
        public void BestuurderTest2_GeldigeCtor_PropertiesVeranderen()
        {
            var geboortedatum = new DateTime(1999, 10, 6);
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };

            var bestuurder = new Bestuurder(100,"De Neef", "Olivier", geboortedatum, "99100630515", rijbewijzen, false);

            Assert.Equal(100,bestuurder.Id);
            Assert.Equal("De Neef", bestuurder.Naam);
            Assert.Equal("Olivier", bestuurder.Voornaam);
            Assert.Equal(geboortedatum, bestuurder.Geboortedatum);
            Assert.Equal("99100630515", bestuurder.Rijksregisternummer);
            Assert.Equal(rijbewijzen, bestuurder.GeefRijbewijsTypes());
        }

        [Fact]
        public void ZetIdTest_GeldigeId_BestuurderIdVeranderd()
        {
            _bestuurder.ZetId(132);

            Assert.Equal(132,_bestuurder.Id);
        }

        [Fact]
        public void ZetIdTest_NegatieveId_ThrowsBestuurderException()
        {
            Assert.ThrowsAny<BestuurderException>(()=> _bestuurder.ZetId(-1));
        }


        [Theory]
        [InlineData("De Neef")]
        [InlineData("   De Neef    ")]
        public void ZetNaamTest_GeldigeNaam_BestuurderNaamVeranderd(string naam)
        {
            _bestuurder.ZetNaam(naam);

            Assert.Equal("De Neef", _bestuurder.Naam);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void ZetNaamTest_OngeldigeNaam_ThrowsBestuurderException(string naam)
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.ZetNaam(naam));
        }


        [Theory]
        [InlineData("   Olivier    ")]
        [InlineData("Olivier")]
        public void ZetVoornaamTest_GeldigeVoornaam_BestuurderVoornaamVeranderd(string voornaam)
        {
            _bestuurder.ZetVoornaam(voornaam);

            Assert.Equal("Olivier", _bestuurder.Voornaam);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void ZetVoornaamTest_OngeldigeVoornaam_ThrowsBestuurderException(string voornaam)
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.ZetVoornaam(voornaam));
        }

        [Fact]
        public void ZetGeboortedatumTest_GeldigeGeboortedatum_BestuurderGeboortedatumVeranderd()
        {
            var geboortedatum = new DateTime(1999, 10, 6);
            _bestuurder.ZetGeboortedatum(geboortedatum);

            Assert.Equal( geboortedatum, _bestuurder.Geboortedatum );
        }

        [Theory]
        [InlineData(9)]
        public void ZetGeboortedatumTest_OngeldigeGeboortedatum_ThrowsBestuurderException(int years)
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.ZetGeboortedatum(DateTime.Today.AddYears(-years)));
        }

        [Theory]
        [InlineData("99.10.06-305.15")]
        [InlineData("99 10 06 305 15  ")]
        [InlineData("    99100630515    ")]
        [InlineData("99100630515")]
        public void ZetRijksregisternummer_GeldigeRijksregisternummer_BestuurderRijksregisternummerVeranderd(string rijksregisternummer)
        {
            _bestuurder.ZetGeboortedatum(new DateTime(1999,10,06));
            _bestuurder.ZetRijksregisternummer(rijksregisternummer);

            Assert.Equal("99100630515", _bestuurder.Rijksregisternummer);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("99110630515")]
        [InlineData("99100699915")]
        [InlineData("99100630514")]
        [InlineData("99100630dd4")]
        [InlineData("991006305142163")]
        public void ZetRijksregisternummer_OnGeldigeRijksregisternummer_ThrowsBestuurderException(string rijksregisternummer)
        {
            _bestuurder.ZetGeboortedatum(new DateTime(1999, 10, 06));

            Assert.Throws<BestuurderException>(()=> _bestuurder.ZetRijksregisternummer(rijksregisternummer));
        }

        [Fact]
        public void ZetAdresTest_GeldigAdres_BestuurdersAdresVeranderd()
        {
            var adres = new Adres("Rosstraat","65","Dendermonde","9200","Belgie");
            adres.ZetLand("belgie");
            _bestuurder.ZetAdres(adres);

            Assert.Equal(adres, _bestuurder.Adres);
        }

        [Fact]
        public void ZetAdresTest_OnGeldigAdres_ThrowsBeException()
        {
            Assert.Throws<BestuurderException>(() => _bestuurder.ZetAdres(null));
        }

        [Fact]
        public void VerwijderAdresTest_GeldigAdres_BestuurdersAdresVeranderd()
        {
            var adres = new Adres("Rosstraat", "65", "Dendermonde", "9200", "Belgie");
            _bestuurder.ZetAdres(adres);

            Assert.Equal(adres,_bestuurder.Adres);

            _bestuurder.VerwijderAdres();

            Assert.Null(_bestuurder.Adres);
        }

        [Fact]
        public void VerwijderAdresTest_AdresIsAlNull_ThrowBestuurderException()
        {
            Assert.Null(_bestuurder.Adres);
            Assert.Throws<BestuurderException>(() => _bestuurder.VerwijderAdres());
        }

        [Fact]
        public void HeeftRijbewijsTypeTest_HeeftRijbewijsType_ReturnsTrue()
        {
            var rijbewijsType = new RijbewijsType("C");
            _bestuurder.VoegRijbewijsTypeToe(rijbewijsType);
            var result = _bestuurder.HeeftRijbewijsType(rijbewijsType);

            Assert.True(result);
        }

        [Fact]
        public void HeeftRijbewijsTypeTest_HeeftRijbewijsTypeNiet_ReturnsFalse()
        {
            var result = _bestuurder.HeeftRijbewijsType(new RijbewijsType("b"));

            Assert.False(result);
        }

        [Fact]
        public void HeeftRijbewijsTypeTest_OngeldigRijbewijstype_ThrowBestuurderException()
        {
            Assert.Throws<BestuurderException>(()=> _bestuurder.HeeftRijbewijsType(null));
        }

        [Fact()]
        public void ToevoegenRijbewijsTypeTest_NogNietInDeLijst_RijbewijsTypeToegvoegdAanBestuurder()
        {
            var rijbewijsType = new RijbewijsType("b");
            _bestuurder.VoegRijbewijsTypeToe(rijbewijsType);

            Assert.True(_bestuurder.HeeftRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void VoegRijbewijsTypeToeTest_AlInDeLijst_ThrowsBestuurdersExcetion()
        {
            var rijbewijsType = new RijbewijsType("b");
            _bestuurder.VoegRijbewijsTypeToe(rijbewijsType);

            Assert.Throws<BestuurderException>(()=> _bestuurder.VoegRijbewijsTypeToe(rijbewijsType));
        }

        [Fact]
        public void VoegRijbewijsTypeToeTest_OngeldigRijbewijsType_ThrowsBestuurderException()
        {
            Assert.Throws<BestuurderException>(() => _bestuurder.VoegRijbewijsTypeToe(null));
        }


        [Fact]
        public void VerwijderRijbewijsTypeTest_RijbewijsTypeIsInDeLijst_RijbewijsTypeVerwijderdBijBestuurder()
        {
            var rijbewijsType = new RijbewijsType("b");
            _bestuurder.VoegRijbewijsTypeToe(rijbewijsType);

            Assert.True(_bestuurder.HeeftRijbewijsType(rijbewijsType));

            _bestuurder.VerwijderRijbewijsType(rijbewijsType);

            Assert.False(_bestuurder.HeeftRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void VerwijderRijbewijsTypeTest_RijbewijsTypeNietInDeLijst_ThrowBestuurderException()
        {
            var rijbewijsType = new RijbewijsType("b");

            Assert.Throws<BestuurderException>(() => _bestuurder.VerwijderRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void VerwijderRijbewijsTypeTest_OngeldigRijbewijsType_ThrowBestuurderException()
        {
            Assert.Throws<BestuurderException>(() => _bestuurder.VerwijderRijbewijsType(null));
        }

      

        [Fact()]
        public void ZetTankkaartTest_GeldigeTankkaart_TankkaartVeranderd()
        {
            var tankkaart = new Tankkaart(1, "123", DateTime.Now);
            var tankkaart2 = new Tankkaart(2, "123", DateTime.Now);
            _bestuurder.ZetTankkaart(tankkaart);

            Assert.Null(tankkaart2.Bestuurder);
            Assert.Equal(tankkaart, _bestuurder.Tankkaart);
            Assert.Equal(_bestuurder, tankkaart.Bestuurder);


            _bestuurder.ZetTankkaart(tankkaart2);

            Assert.Null(tankkaart.Bestuurder);
            Assert.Equal(tankkaart2, _bestuurder.Tankkaart);
            Assert.Equal(_bestuurder, tankkaart2.Bestuurder);

        }

        [Fact()]
        public void ZetTankkaartTest_TankkaartIsDezelfde_ThrowBestuurderException()
        {
            List<RijbewijsType> rijbewijzen = new() { new RijbewijsType("B") };
            var sameBestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", rijbewijzen);
            var date = DateTime.Now;
            var tankkaart = new Tankkaart(1, "123", date);

            _bestuurder.ZetTankkaart(tankkaart);

            Assert.Equal(tankkaart, _bestuurder.Tankkaart);

            Assert.Throws<BestuurderException>(() => _bestuurder.ZetTankkaart(tankkaart));
        }

        [Fact()]
        public void ZetTankkaartTest_TankkaartIsNull_ThrowBestuurderException()
        {
            Assert.Throws<BestuurderException>(() => _bestuurder.ZetTankkaart(null));
        }

        [Fact()]
        public void ZetVoertuigTest_GeldigVoetuig_BestuurdersVoertuigVeranderd()
        {
            var auto = new Voertuig("Bmw","X5","wauzzz8v5ka106598","1-abc-123",new BrandstofType("diezel"),new WagenType("Auto"));
            var auto1 = new Voertuig("Bmw", "X5", "wauzzz8v5ka106598", "1-abc-123", new BrandstofType("diezel"), new WagenType("Auto"));
            _bestuurder.ZetVoertuig(auto);

            Assert.Equal(auto, _bestuurder.Voertuig);
            Assert.Equal(_bestuurder, auto.Bestuurder);
            Assert.Null(auto1.Bestuurder);

            _bestuurder.ZetVoertuig(auto1);

           
            Assert.Equal(auto1, _bestuurder.Voertuig);
            Assert.Equal(_bestuurder, auto1.Bestuurder);
            Assert.Null(auto.Bestuurder);
        }

        [Fact()]
        public void ZetVoertuigTest_OnGeldigVoetuig_ThrowBestuurderException()
        {
            Assert.Throws<BestuurderException>(() => _bestuurder.ZetVoertuig(null));
        }


        [Fact()]
        public void ZetVoertuigTest_ZelfdeVoertuig_ThrowBestuurderException()
        {
            var auto = new Voertuig("Bmw", "X5", "wauzzz8v5ka106598", "1-abc-123", new BrandstofType("diezel"), new WagenType("Auto"));
            _bestuurder.ZetVoertuig(auto);
            Assert.Equal(auto , _bestuurder.Voertuig);
            Assert.Throws<BestuurderException>(() => _bestuurder.ZetVoertuig(auto));
        }


        [Fact]
        public void ZetGearchiveerdTest_True_ArchieveerBestuurder()
        {
            var auto = new Voertuig("Bmw", "X5", "wauzzz8v5ka106598", "1-abc-123", new BrandstofType("diezel"), new WagenType("Auto"));
            var tankkaart = new Tankkaart(1, "123", DateTime.Now);
            _bestuurder.ZetTankkaart(tankkaart);
            _bestuurder.ZetVoertuig(auto);

            Assert.False(_bestuurder.IsGearchiveerd);
            Assert.Equal(auto,_bestuurder.Voertuig);
            Assert.Equal(tankkaart,_bestuurder.Tankkaart);

            _bestuurder.ZetGearchiveerd(true);
            
            Assert.True(_bestuurder.IsGearchiveerd);
            Assert.Null(_bestuurder.Voertuig);
            Assert.Null(_bestuurder.Tankkaart);
        }

        [Fact]
        public void ZetGearchiveerdTest_False_DeArchieveren()
        {
            Assert.False(_bestuurder.IsGearchiveerd);

            _bestuurder.ZetGearchiveerd(true);

            Assert.True(_bestuurder.IsGearchiveerd);
            Assert.Null(_bestuurder.Voertuig);
            Assert.Null(_bestuurder.Tankkaart);

            _bestuurder.ZetGearchiveerd(false);

            Assert.False(_bestuurder.IsGearchiveerd);
        }

        [Fact]
        public void VerwijderTankkaartTest_TankkaartIsNietNull_ZetTankkaartOpNull()
        {
            var tankkaart = new Tankkaart(1,"123",DateTime.Now);
            _bestuurder.ZetTankkaart(tankkaart);

            Assert.Equal(tankkaart , _bestuurder.Tankkaart);
            Assert.Equal(_bestuurder, tankkaart.Bestuurder);

            _bestuurder.VerwijderTankkaart();

            Assert.Null(_bestuurder.Tankkaart);
            Assert.Null(tankkaart.Bestuurder);
        }

        [Fact]
        public void VerwijderTankkaartTest_TankkaartIsAlNull_ThrowsBestuurderException()
        {
            Assert.Null(_bestuurder.Tankkaart);
            Assert.Throws<BestuurderException>(() => _bestuurder.VerwijderTankkaart()) ;
        }

        [Fact]
        public void VerwijderVoertuigTest_VoertuigIsNietNull_ZetVoertuigOpNull()
        {
            var voertuig = new Voertuig("Bmw", "X5", "wauzzz8v5ka106598", "1-abc-123", new BrandstofType("diezel"), new WagenType("Auto"));
            _bestuurder.ZetVoertuig(voertuig);

            Assert.Equal(voertuig, _bestuurder.Voertuig);
            Assert.Equal(_bestuurder,voertuig.Bestuurder);

            _bestuurder.VerwijderVoertuig();

            Assert.Null(_bestuurder.Voertuig);
            Assert.Null(voertuig.Bestuurder);
        }

        [Fact]
        public void VerwijderVoertuigTest_VoertuigIsAlNull_ThrowBestuurderException()
        {
            Assert.Null(_bestuurder.Voertuig);
            Assert.Throws<BestuurderException>(() =>_bestuurder.VerwijderVoertuig());
        }
    }
}