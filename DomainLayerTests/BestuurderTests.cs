using System;
using DomainLayer;
using DomainLayer.Exceptions;
using Xunit;
using Xunit.Sdk;

namespace DomainLayerTests
{
    public class BestuurderTests
    {
        private readonly Bestuurder _bestuurder;
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
        public void SetGeboortedatumTest_GeldigeGeboortedatum_BestuurderGebrootedatumVeranderd()
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

        [Fact()]
        public void HasRijbewijsTypeTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void AddRijbewijsTypeTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void RemoveRijbewijsTypeTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void SetStraatTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void SetHuisnummerTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void SetStadTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void SetLandTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void SetTankkaartTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void SetVoertuigTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void SetDeletedTest()
        {
            throw new NotImplementedException();
        }
    }
}