using System;
using System.Collections.Generic;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class TankkaartTests
    {
        /// <summary>
        /// Todo : Zet instantiation van tankkaart in ctor
        /// Todo : ZetPincode uitgebeider testen
        /// Todo : ZetBrandstofType uitgebeider testen
        /// Todo : VoegToeBrandstofType uitgebeider testen
        /// Todo : VerwijderBrandstofType uitgebeider testen
        /// Todo : Zetbestuurder uitgebeider testen
        /// Todo : BlokkeerKaart uitgebeider testen
        /// </summary>


        public TankkaartTests()
        {
            
        }

        [Fact]
        public void Test_ctor_valid_noBestuurder_noBrandstoftypes_noPincode()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            Assert.Equal(5, tankkaart.Id);
            Assert.Equal("123ABC98", tankkaart.Kaartnummer);
            Assert.Equal(new DateTime(2022, 12, 31), tankkaart.Geldigheidsdatum);
        }

        [Fact]
        public void Test_ctor_valid()
        {
            List<BrandstofType> brandstofTypes = new() {new BrandstofType() };
            Bestuurder bestuurder = new Bestuurder();

            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31), "1111", brandstofTypes, bestuurder);

            Assert.Equal(5, tankkaart.Id);
            Assert.Equal("123ABC98", tankkaart.Kaartnummer);
            Assert.Equal(new DateTime(2022, 12, 31), tankkaart.Geldigheidsdatum);
            Assert.Equal("1111", tankkaart.Pincode);
            Assert.Equal(brandstofTypes, tankkaart.GeefBrandstofTypes());
            Assert.Equal(bestuurder, tankkaart.Bestuurder);
        }

        [Fact]
        public void Test_ZetId_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            tankkaart.ZetId(8);
            Assert.Equal(8, tankkaart.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_invalid(int id)
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            Assert.Throws<TankkaartException>(() => tankkaart.ZetId(id));
            
        }

        [Fact]
        public void Test_ZetKaartnummer_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            tankkaart.ZetKaartnummer("789XYZ12");
            Assert.Equal("789XYZ12", tankkaart.Kaartnummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        public void Test_ZetKaartnummer_invalid(string kaartnummer)
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            Assert.Throws<TankkaartException>(() => tankkaart.ZetKaartnummer(kaartnummer));
        }



    }
}