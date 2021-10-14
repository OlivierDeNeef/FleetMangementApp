using System;
using DomainLayer.Exceptions.Models;
using DomainLayer.Exceptions.Utilities;
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
            _bestuurder =  new Bestuurder();
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

        [Fact]
        public void ZetNaamTest_OngeldigeNaam_ThrowsBestuurderException()
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.ZetNaam("    "));
        }


        [Theory]
        [InlineData("   Olivier    ")]
        [InlineData("Olivier")]
        public void ZetVoornaamTest_GeldigeVoornaam_BestuurderVoornaamVeranderd(string voornaam)
        {
            _bestuurder.ZetVoornaam(voornaam);

            Assert.Equal("Olivier", _bestuurder.Voornaam);
        }


        [Fact]
        public void ZetVoornaamTest_OngeldigeVoornaam_ThrowsBestuurderException()
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.ZetVoornaam("    "));
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

            Assert.Throws<RijksregisternummerCheckerException>(()=> _bestuurder.ZetRijksregisternummer(rijksregisternummer));
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
            adres.ZetLand("belgie");
            _bestuurder.ZetAdres(adres);
            _bestuurder.VerwijderAdres();

            Assert.Null(_bestuurder.Adres);
        }

        [Fact]
        public void HeeftRijbewijsTypeTest_HeeftRijbewijsType_ReturnsTrue()
        {
            var rijbewijsType = new RijbewijsType(){Id = 1, Type = "B"};
            _bestuurder.VoegRijbewijsTypeToe(rijbewijsType);
            var result = _bestuurder.HeeftRijbewijsType(rijbewijsType);

            Assert.True(result);
        }

        [Fact]
        public void HeeftRijbewijsTypeTest_HeeftRijbewijsTypeNiet_ReturnsFalse()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
            var result = _bestuurder.HeeftRijbewijsType(rijbewijsType);

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
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
            _bestuurder.VoegRijbewijsTypeToe(rijbewijsType);

            Assert.True(_bestuurder.HeeftRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void VoegRijbewijsTypeToeTest_AlInDeLijst_ThrowsBestuurdersExcetion()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
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
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
            _bestuurder.VoegRijbewijsTypeToe(rijbewijsType);
            _bestuurder.VerwijderRijbewijsType(rijbewijsType);

            Assert.False(_bestuurder.HeeftRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void VerwijderRijbewijsTypeTest_RijbewijsTypeNietInDeLijst_ThrowBestuurderException()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };

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
            _bestuurder.ZetTankkaart(tankkaart2);

            Assert.Null(tankkaart.Bestuurder);
            Assert.Equal(tankkaart2, _bestuurder.Tankkaart);
            Assert.Equal(_bestuurder, tankkaart2.Bestuurder);

        }

        [Fact()]
        public void ZetVoertuigTest_GeldigVoetuig_BestuurdersVoertuigVeranderd()
        {
            var auto = new Voertuig();
            var auto1 = new Voertuig();
            _bestuurder.ZetVoertuig(auto);
            _bestuurder.ZetVoertuig(auto1);

           
            Assert.Equal(auto1, _bestuurder.Voertuig);
            Assert.Equal(_bestuurder, auto1.Bestuurder);
            Assert.Null(auto.Bestuurder);
        }

        [Fact()]
        public void ZetVoertuigTest_OnGeldigVoetuig_ThrowBestuurderException()
        {
            Voertuig auto = null;
            Assert.Throws<BestuurderException>(() => _bestuurder.ZetVoertuig(auto));
        }


        [Fact()]
        public void ZetVoertuigTest_ZelfdeVoertuig_ThrowBestuurderException()
        {
            var auto = new Voertuig();
            _bestuurder.ZetVoertuig(auto);
            Assert.Throws<BestuurderException>(() => _bestuurder.ZetVoertuig(auto));
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void ZetGearchiveerdTest_VeranderdBestuurderIsDeleted(bool isDeleted)
        {
            _bestuurder.ZetGearchiveerd(isDeleted);
            
            Assert.Equal(isDeleted , _bestuurder.IsGearchiveerd);
        }

        [Fact]
        public void VerwijderTankkaartTest_TankkaartIsNietNull_ZetTankkaartOpNull()
        {
            var tankkaart = new Tankkaart(1,"123",DateTime.Now);
            _bestuurder.ZetTankkaart(tankkaart);
            Assert.Equal(tankkaart , _bestuurder.Tankkaart);
            _bestuurder.VerwijderTankkaart();
            Assert.Null(_bestuurder.Tankkaart);
        }

        [Fact]
        public void VerwijderTankkaartTest_TankkaartIsAlNull_ThrowsBestuurderException()
        {
            var tankkaart = new Tankkaart(1, "123", DateTime.Now);
            Assert.Throws<BestuurderException>(() => _bestuurder.VerwijderTankkaart()) ;
        }

        [Fact]
        public void VerwijderVoertuigTest_VoertuigIsNietNull_ZetVoertuigOpNull()
        {
            var voertuig = new Voertuig();
            _bestuurder.ZetVoertuig(voertuig);
            Assert.Equal(voertuig, _bestuurder.Voertuig);
            _bestuurder.VerwijderVoertuig();
            Assert.Null(_bestuurder.Voertuig);

        }

        [Fact]
        public void VerwijderVoertuigTest_VoertuigIsAlNull_ThrowBestuurderException()
        {
            var voertuig = new Voertuig();
            Assert.Throws<BestuurderException>(() =>_bestuurder.VerwijderVoertuig());
        }
    }
}