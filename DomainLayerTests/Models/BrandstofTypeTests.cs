using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class BrandstofTypeTests
    {
        private readonly BrandstofType _brandstofType;

        public BrandstofTypeTests()
        {
            _brandstofType = new BrandstofType("Benzine");
        }

        [Fact()]
        public void ZetIdTest_GeldigId_IdVeranderd()
        {
            _brandstofType.ZetId(3);
            Assert.Equal(3,_brandstofType.Id);
        }

        [Fact()]
        public void ZetIdTest_OnGeldigId_ThrowsBrandstofTypeException()
        {
            Assert.Throws<BrandstofTypeException>(() => _brandstofType.ZetId(0));
        }

        [Fact()]
        public void ZetTypeTest_GeldigType_TypeVeranderd()
        {
            _brandstofType.ZetType("Diesel");
            Assert.Equal("DIESEL",_brandstofType.Type);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Benzine")]
        public void ZetTypeTest_OnGeldigType_ThrowsBranstofTypeException(string type)
        {
            Assert.Equal("BENZINE",_brandstofType.Type);
            Assert.Throws<BrandstofTypeException>(() => _brandstofType.ZetType(type));
        }
    }
}