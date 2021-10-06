using System;
using DomainLayer;
using DomainLayer.Exceptions;
using Xunit;

namespace DomainLayerTests
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
        public void SetIdTest_GeldigeId_BestuurderIdVeranderd()
        {
            _bestuurder.SetId(132);

            Assert.Equal(132,_bestuurder.Id);
        }

        [Fact]
        public void SetIdTest_NegatieveId_ThrowsBestuurderException()
        {
            Assert.ThrowsAny<BestuurderException>(()=> _bestuurder.SetId(-1));
        }


        [Theory]
        [InlineData("De Neef")]
        [InlineData("   De Neef    ")]
        public void SetNaamTest_GeldigeNaam_BestuurderNaamVeranderd(string naam)
        {
            _bestuurder.SetNaam(naam);

            Assert.Equal("De Neef", _bestuurder.Naam);
        }

        [Fact]
        public void SetNaamTest_OngeldigeNaam_ThrowsBestuurderException()
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.SetNaam("    "));
        }


        [Theory]
        [InlineData("   Olivier    ")]
        [InlineData("Olivier")]
        public void SetVoornaamTest_GeldigeVoornaam_BestuurderVoornaamVeranderd(string voornaam)
        {
            _bestuurder.SetVoornaam(voornaam);

            Assert.Equal("Olivier", _bestuurder.Voornaam);
        }


        [Fact]
        public void SetVoornaamTest_OngeldigeVoornaam_ThrowsBestuurderException()
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.SetVoornaam("    "));
        }


        [Fact]
        public void SetGeboortedatumTest_GeldigeGeboortedatum_BestuurderGeboortedatumVeranderd()
        {
            var geboortedatum = new DateTime(1999, 10, 6);
            _bestuurder.SetGeboortedatum(geboortedatum);

            Assert.Equal( geboortedatum, _bestuurder.Geboortedatum );
        }

        [Theory]
        [InlineData(110)]
        [InlineData(9)]
        public void SetGeboortedatumTest_OngeldigeGeboortedatum_ThrowsBestuurderException(int years)
        {
            Assert.ThrowsAny<BestuurderException>(() => _bestuurder.SetGeboortedatum(DateTime.Today.AddYears(-years)));
        }

        [Theory]
        [InlineData("99.10.06-305.15")]
        [InlineData("99 10 06 305 15  ")]
        [InlineData("    99100630515    ")]
        [InlineData("99100630515")]
        public void SetRijksregisternummer_GeldigeRijksregisternummer_BestuurderRijksregisternummerVeranderd(string rijksregisternummer)
        {
            _bestuurder.SetGeboortedatum(new DateTime(1999,10,06));
            _bestuurder.SetRijksregisternummer(rijksregisternummer);

            Assert.Equal("99100630515", _bestuurder.Rijksregisternummer);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("99110630515")]
        [InlineData("99100699915")]
        [InlineData("99100630514")]
        [InlineData("99100630dd4")]
        [InlineData("991006305142163")]
        public void SetRijksregisternummer_OnGeldigeRijksregisternummer_ThrowsBestuurderException(string rijksregisternummer)
        {
            _bestuurder.SetGeboortedatum(new DateTime(1999, 10, 06));

            Assert.Throws<BestuurderException>(()=> _bestuurder.SetRijksregisternummer(rijksregisternummer));
        }

        [Fact]
        public void HasRijbewijsTypeTest_HeeftRijbewijsType_ReturnsTrue()
        {
            var rijbewijsType = new RijbewijsType(){Id = 1, Type = "B"};
            _bestuurder.AddRijbewijsType(rijbewijsType);
            var result = _bestuurder.HasRijbewijsType(rijbewijsType);

            Assert.True(result);
        }

        [Fact]
        public void HasRijbewijsTypeTest_HeeftRijbewijsTypeNiet_ReturnsFalse()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
            var result = _bestuurder.HasRijbewijsType(rijbewijsType);

            Assert.False(result);
        }

        [Fact]
        public void HasRijbewijsTypeTest_OngeldigRijbewijstype_ThrowBestuurderException()
        {
            Assert.Throws<BestuurderException>(()=> _bestuurder.HasRijbewijsType(null));
        }

        [Fact()]
        public void AddRijbewijsTypeTest_NogNietInDeLijst_RijbewijsTypeToegvoegdAanBestuurder()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
            _bestuurder.AddRijbewijsType(rijbewijsType);

            Assert.True(_bestuurder.HasRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void AddRijbewijsTypeTest_AlInDeLijst_ThrowsBestuurdersExcetion()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
            _bestuurder.AddRijbewijsType(rijbewijsType);

            Assert.Throws<BestuurderException>(()=> _bestuurder.AddRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void AddRijbewijsTypeTest_OngeldigRijbewijsType_ThrowsBestuurderException()
        {
            Assert.Throws<BestuurderException>(() => _bestuurder.AddRijbewijsType(null));
        }


        [Fact]
        public void RemoveRijbewijsTypeTest_RijbewijsTypeIsInDeLijst_RijbewijsTypeVerwijderdBijBestuurder()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };
            _bestuurder.AddRijbewijsType(rijbewijsType);
            _bestuurder.RemoveRijbewijsType(rijbewijsType);

            Assert.False(_bestuurder.HasRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void RemoveRijbewijsTypeTest_RijbewijsTypeNietInDeLijst_ThrowBestuurderException()
        {
            var rijbewijsType = new RijbewijsType() { Id = 1, Type = "B" };

            Assert.Throws<BestuurderException>(() => _bestuurder.RemoveRijbewijsType(rijbewijsType));
        }

        [Fact]
        public void RemoveRijbewijsTypeTest_OngeldigRijbewijsType_ThrowBestuurderException()
        {
            Assert.Throws<BestuurderException>(() => _bestuurder.RemoveRijbewijsType(null));
        }

        [Theory]
        [InlineData("     test    ","test")]
        [InlineData("test","test")]
        [InlineData("","")]
        [InlineData(null,null)]
        public void SetStraatTest_GeldigeStraat_BestuurderStraatVeranderd(string straat, string output)
        {
            _bestuurder.SetStraat(straat);

            Assert.Equal(output,_bestuurder.Straat);
        }


        [Theory]
        [InlineData("","")]
        [InlineData(null,null)]
        [InlineData("2     ","2")]
        [InlineData("2A","2A")]
        public void SetHuisnummerTest_GeldigHuisnummer_BestuurderHuisnummerVeranderd(string huisnummer, string output)
        {
            _bestuurder.SetHuisnummer(huisnummer);

            Assert.Equal(output,_bestuurder.Huisnummer);
        }

        [Theory]
        [InlineData("   Gent", "Gent")]
        [InlineData("Gent", "Gent")]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void SetStadTest_GeldigeStad_BestuurdersStadVeranderd(string stad, string output)
        {
            _bestuurder.SetStad(stad);

            Assert.Equal(output,_bestuurder.Stad);
        }

        [Theory]
        [InlineData("belgie","belgie")]
        [InlineData("   belgie","belgie")]
        [InlineData(null,null)]
        public void SetLandTest_GeldigLand_BestuurdersLandVeranderd(string land , string output)
        {
            _bestuurder.SetLand(land);

            Assert.Equal(output, _bestuurder.Land);
        }

        [Fact()]
        public void SetTankkaartTest()
        {
            //Todo: test voor set Tankkart
        }

        [Fact()]
        public void SetVoertuigTest()
        {
            //Todo: test voor set voertuig
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public void SetDeletedTest_VeranderdBestuurderIsDeleted(bool isDeleted)
        {
            _bestuurder.SetDeleted(isDeleted);
            
            Assert.Equal(isDeleted , _bestuurder.IsDeleted);
        }
    }
}