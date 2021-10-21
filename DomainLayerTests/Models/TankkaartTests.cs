using System;
using System.Collections.Generic;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    public class TankkaartTests
    {

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
            List<BrandstofType> brandstofTypes = new() {new BrandstofType("benzine") };
            Bestuurder bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",new List<RijbewijsType>());

            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder);

            Assert.Equal(5, tankkaart.Id);
            Assert.Equal("123ABC98", tankkaart.Kaartnummer);
            Assert.Equal(new DateTime(2022, 12, 31), tankkaart.Geldigheidsdatum);
            Assert.Equal("1111", tankkaart.Pincode);
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

        [Fact]
        public void Test_ZetPincode_valid()
        {
            Bestuurder bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", new List<RijbewijsType>());
            

            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder);

            tankkaart.ZetPincode("1234");

            Assert.Equal("1234",tankkaart.Pincode);
        }

        [Theory]
        [InlineData("")]
        [InlineData("ABCD")]
        [InlineData("12345")]
        [InlineData("123")]
        public void Test_ZetPincode_invalid(string pincode)
        {
            Bestuurder bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", new List<RijbewijsType>());
            

            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder);

            Assert.Throws<TankkaartException>(() => tankkaart.ZetPincode(pincode));
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            BrandstofType brandstofType = new BrandstofType("Euro95");
            
            tankkaart.VoegBrandstofTypeToe(brandstofType);
            Assert.Equal(brandstofType,tankkaart.GeefBrandstofTypes()[0]);
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_invalid_brandstofNull()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            BrandstofType brandstofType = null;

            Assert.Throws<TankkaartException>(() => tankkaart.VoegBrandstofTypeToe(brandstofType));
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_invalid_brandstofBestaatAl()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            BrandstofType diesel = new BrandstofType("Diesel");
            tankkaart.VoegBrandstofTypeToe(diesel);

            Assert.Throws<TankkaartException>(() => tankkaart.VoegBrandstofTypeToe(diesel));
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_invalid_brandstofMetTypeBestaatAl()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            BrandstofType diesel = new BrandstofType("Diesel");
            tankkaart.VoegBrandstofTypeToe(diesel);
            BrandstofType diesel2 = new BrandstofType("Diesel");
            Assert.Throws<TankkaartException>(() => tankkaart.VoegBrandstofTypeToe(diesel2));
        }

        [Fact]
        public void Test_VerwijderBrandstofType_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            BrandstofType brandstofType = new BrandstofType("Diesel");

            tankkaart.VoegBrandstofTypeToe(brandstofType);
            tankkaart.VerwijderBrandstofType(brandstofType);
            Assert.DoesNotContain(brandstofType,tankkaart.GeefBrandstofTypes());
        }

        [Fact]
        public void Test_VerwijderBrandstofType_invalid_brandstofTypeNull()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            BrandstofType brandstofType = null;

            Assert.Throws<TankkaartException>(() => tankkaart.VerwijderBrandstofType(brandstofType));
        }

        [Fact]
        public void Test_VerwijderBrandstofType_invalid_brandstofTypeNietoptankkaart()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            BrandstofType brandstofType = new BrandstofType("Diesel");

            Assert.Throws<TankkaartException>(() => tankkaart.VerwijderBrandstofType(brandstofType));
        }

        [Fact]
        public void Test_ZetBestuurder_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            Bestuurder bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", new List<RijbewijsType>());

            tankkaart.ZetBestuurder(bestuurder);
            Assert.Equal(bestuurder, tankkaart.Bestuurder);
        }

        [Fact]
        public void Test_ZetBestuurder_invalid_bestuurderisnull()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            Bestuurder bestuurder = null;

            Assert.Throws<TankkaartException>(() => tankkaart.ZetBestuurder(bestuurder));
        }


        [Fact]
        public void Test_VerwijderBestuurder_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            Bestuurder bestuurder = new Bestuurder("De Neef","Olivier",new DateTime(1999,10,6),"99100630515",new List<RijbewijsType>());

            tankkaart.ZetBestuurder(bestuurder);
            tankkaart.VerwijderBestuurder();

            Assert.Null(tankkaart.Bestuurder);
        }

        [Fact]
        public void Test_VerwijderBestuurder_invalid_bestuurderNull()
        {
            Tankkaart tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));

            Assert.Throws<TankkaartException>(() => tankkaart.VerwijderBestuurder());
        }

        [Fact]
        public void Test_BlokkeerKaart_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            tankkaart.BlokkeerKaart();
            Assert.True(tankkaart.IsGeblokkeerd);
        }

        [Fact]
        public void Test_BlokkeerKaart_invalid()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            tankkaart.BlokkeerKaart();
            Assert.Throws<TankkaartException>(() => tankkaart.BlokkeerKaart());
        }

        [Fact]
        public void Test_DeblokkeerKaart_valid()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            tankkaart.BlokkeerKaart();
            tankkaart.DeblokkeerKaart();
            Assert.False(tankkaart.IsGeblokkeerd);
        }

        [Fact]
        public void Test_DeblokkeerKaart_invalid()
        {
            Tankkaart tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.Throws<TankkaartException>(() => tankkaart.DeblokkeerKaart());
        }

    }
}