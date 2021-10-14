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
            _adres = new Adres();
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
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ZetStraatTest_GeldigeStraat_AdresStraatVeranderd(string straat, string output)
        {
            _adres.ZetStraat(straat);

            Assert.Equal(output, _adres.Straat);
        }


        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("2     ", "2")]
        [InlineData("2A", "2A")]
        public void ZetHuisnummerTest_GeldigHuisnummer_AdresHuisnummerVeranderd(string huisnummer, string output)
        {
            _adres.ZetHuisnummer(huisnummer);

            Assert.Equal(output, _adres.Huisnummer);
        }

        [Theory]
        [InlineData("   Gent", "Gent")]
        [InlineData("Gent", "Gent")]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void ZetStadTest_GeldigeStad_AdresStadVeranderd(string stad, string output)
        {
            _adres.ZetStad(stad);

            Assert.Equal(output, _adres.Stad);
        }

        [Theory]
        [InlineData("belgie", "belgie")]
        [InlineData("   belgie", "belgie")]
        [InlineData(null, null)]
        public void ZetLandTest_GeldigLand_AdresLandVeranderd(string land, string output)
        {
            _adres.ZetLand(land);

            Assert.Equal(output, _adres.Land);
        }

        [Theory]
        [InlineData("9200", "9200")]
        [InlineData("   9200", "9200")]
        [InlineData(null, null)]
        public void ZetPostcodeTest_GeldigePostcode_AdresPostcodeveranderd(string postcode, string output)
        {
            _adres.ZetPostcode(postcode);

            Assert.Equal(output, _adres.Postcode);
        }
    }
}