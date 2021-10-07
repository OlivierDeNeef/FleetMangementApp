using Xunit;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;

namespace DomainLayer.Tests
{
    public class VoertuigTests
    {
        private readonly Voertuig _voertuig;

      
        public VoertuigTests()
        {
            _voertuig = new Voertuig();
        } 
        [Fact()]
        public void SetIdTest()
        {
            _voertuig.SetId(4);
            Assert.Equal(4, _voertuig.Id);

        }

        [Fact()]
        public void SetId_NegatieveId_ThrowException()
        {
            Assert.ThrowsAny<VoertuigExceptions>(() => _voertuig.SetId(-4));
        }

        [Fact()]
        public void SetMerkTest()
        {
           _voertuig.SetMerk("Mercedez");
           Assert.Equal("Mercedez", _voertuig.Merk);
        }
        

        [Fact()]
        public void SetModelTest()
        {
            _voertuig.SetModel("A-Klasse");
            Assert.Equal("A-Klasse", _voertuig.Model);
        }

        [Fact()]
        public void SetChassisnummerTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SetWagenTypeTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SetBrandstofTypeTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SetNummerplaatTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SetKleurTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SetAantalDeurenTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SetBestuurderTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SetIsDeletedTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}