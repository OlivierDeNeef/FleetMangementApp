using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class WagenTypeTests
    {
        public WagenTypeTests()
        {

        }

        [Fact]
        public void Test_ctor_valid_noId()
        {
            var wagenType = new WagenType("Auto");

            Assert.Equal("Auto",wagenType.Type);
        }

        [Fact]
        public void Test_ctor_valid()
        {
            var wagenType = new WagenType(123,"Vrachtwagen");

            Assert.Equal(123,wagenType.Id);
            Assert.Equal("Vrachtwagen",wagenType.Type);
        }

        [Fact]
        public void Test_ZetId_valid()
        {
            var wagenType = new WagenType(123,"Vrachtwagen");

            Assert.Equal(123,wagenType.Id);

            wagenType.ZetId(456);

            Assert.Equal(456,wagenType.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_invalid(int id)
        {
            var wagenType = new WagenType(123,"Vrachtwagen");

            Assert.Throws<WagenTypeException>(() => wagenType.ZetId(id));
        }

        [Theory]
        [InlineData("Bestelwagen")]
        [InlineData("Bestelwagen              ")]
        [InlineData("      Bestelwagen")]
        public void Test_ZetType_valid(string type)
        {
            var wagenType = new WagenType(123,"Vrachtwagen");

            Assert.Equal("Vrachtwagen", wagenType.Type);

            wagenType.ZetType(type);

            Assert.Equal("Bestelwagen",wagenType.Type);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("Vrachtwagen")]
        public void Test_ZetType_invalid(string type)
        {
            var wagenType = new WagenType(123,"Vrachtwagen");

            Assert.Throws<WagenTypeException>(() => wagenType.ZetType(type));
        }

    }
}
