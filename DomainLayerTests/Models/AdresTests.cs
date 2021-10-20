using DomainLayer;
using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class AdresTests
    {
        private readonly Adres _adres;

        public AdresTests()
        {
            _adres = new Adres("Rosstraat","65","Dendermonde","9200","Belgie");
        }



        [Fact]
        public void ZetIdTest_GeldigeId_AdresIdVeranderd()
        {
            _adres.ZetId(132);

            Assert.Equal(132, _adres.Id);
        }

        [Fact]
        public void ZetIdTest_NegatieveId_ThrowsAdresException()
        {
            Assert.ThrowsAny<AdresException>(() => _adres.ZetId(-1));
        }


        [Theory]
        [InlineData("     test    ", "test")]
        [InlineData("test", "test")]
        public void ZetStraatTest_GeldigeStraat_AdresStraatVeranderd(string straat, string output)
        {
            _adres.ZetStraat(straat);

            Assert.Equal(output, _adres.Straat);
        }

        [Theory]
        [InlineData("     ")]
        [InlineData(null)]
        public void ZetStraatTest_OnGeldigeStraat_ThrowsAdresException(string straat)
        {
            Assert.Throws<AdresException>(() => _adres.ZetStraat(straat));
        }
        [Fact]
        public void ZetBusnummerTest_Valid()
        {
            _adres.ZetBusnummer("a2");
            Assert.Equal("a2", _adres.Busnummer);
        }

        [Theory]
        [InlineData("2     ", "2")]
        [InlineData("2A", "2A")]
        public void ZetHuisnummerTest_GeldigHuisnummer_AdresHuisnummerVeranderd(string huisnummer, string output)
        {
            _adres.ZetHuisnummer(huisnummer);

            Assert.Equal(output, _adres.Huisnummer);
        }

        [Theory]
        [InlineData("    ")]
        [InlineData(null)]
        [InlineData("A")]
        public void ZetHuisnummerTest_OnGeldigHuisnummer_ThrowsAdresException(string huisnummer)
        {
            Assert.Throws<AdresException>(() => _adres.ZetHuisnummer(huisnummer));
        }

        [Theory]
        [InlineData("   Gent", "GENT")]
        [InlineData("Gent", "GENT")]
        public void ZetStadTest_GeldigeStad_AdresStadVeranderd(string stad, string output)
        {
            _adres.ZetStad(stad);

            Assert.Equal(output, _adres.Stad);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData(null)]
        public void ZetStadTest_OnGeldigeStad_ThrowsAdresException(string stad)
        {
            Assert.Throws<AdresException>(() => _adres.ZetStad(stad));
        }

        [Theory]
        [InlineData("belgie", "BELGIE")]
        [InlineData("   belgie", "BELGIE")]
       
        public void ZetLandTest_GeldigLand_AdresLandVeranderd(string land, string output)
        {
            _adres.ZetLand(land);

            Assert.Equal(output, _adres.Land);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData(null)]

        public void ZetLandTest_OnGeldigLand_ThrowsAdresException(string land)
        {
            Assert.Throws<AdresException>(() => _adres.ZetLand(land));
        }

        [Theory]
        [InlineData("9200", "9200")]
        [InlineData("   9200", "9200")]
        public void ZetPostcodeTest_GeldigePostcode_AdresPostcodeveranderd(string postcode, string output)
        {
            _adres.ZetPostcode(postcode);

            Assert.Equal(output, _adres.Postcode);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData(null)]
        public void ZetPostcodeTest_OnGeldigePostcode_ThrowAdresException(string postcode)
        {
            Assert.Throws<AdresException>(()=> _adres.ZetPostcode(postcode));
        }
    }
}