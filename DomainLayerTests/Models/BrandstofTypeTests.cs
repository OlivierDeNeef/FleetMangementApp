using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class BrandstofTypeTests
    {
        private BrandstofType _brandstofType;

        public BrandstofTypeTests()
        {
            _brandstofType = new BrandstofType("Benzine");
        }

        [Fact()]
        public void ZetIdTest_GeldigId_IdVeranderd()
        {
            
        }

        [Fact()]
        public void ZetTypeTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}