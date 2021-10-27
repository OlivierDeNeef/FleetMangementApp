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
        [Fact]
        public void Test_ctor_valid_noBestuurder_noBrandstoftypes_noPincode_noID()
        {
            var tankkaart = new Tankkaart("123ABC98", new DateTime(2022, 12, 31));

            Assert.Equal("123ABC98", tankkaart.Kaartnummer);
            Assert.Equal(new DateTime(2022, 12, 31), tankkaart.Geldigheidsdatum);
        }

        [Fact]
        public void Test_ctor_valid_noBestuurder_noBrandstoftypes_noPincode()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.Equal(5, tankkaart.Id);
            Assert.Equal("123ABC98", tankkaart.Kaartnummer);
            Assert.Equal(new DateTime(2022, 12, 31), tankkaart.Geldigheidsdatum);
        }


        [Fact]
        public void Test_ctor_valid()
        {
            List<BrandstofType> brandstofTypes = new() {new BrandstofType("benzine") };
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",new List<RijbewijsType>());

            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder);

            Assert.Equal(5, tankkaart.Id);
            Assert.Equal("123ABC98", tankkaart.Kaartnummer);
            Assert.Equal(new DateTime(2022, 12, 31), tankkaart.Geldigheidsdatum);
            Assert.Equal("1111", tankkaart.Pincode);
            Assert.Equal(bestuurder, tankkaart.Bestuurder);
        }

        [Fact]
        public void Test_Equals_valid()
        {
            
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",new List<RijbewijsType>());

            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder);

            List<BrandstofType> brandstofTypes2 = new() {new BrandstofType("benzine") };
            var bestuurder2 = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",new List<RijbewijsType>());

            var tankkaart2 = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder2);

            Assert.True(bestuurder.Equals(bestuurder2));
        }

        [Fact]
        public void Test_ZetId_valid()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.Equal(5,tankkaart.Id);

            tankkaart.ZetId(8);

            Assert.Equal(8, tankkaart.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_ZetId_invalid(int id)
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.Throws<TankkaartException>(() => tankkaart.ZetId(id));
            
        }

        [Fact]
        public void Test_ZetKaartnummer_valid()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.Equal("123ABC98",tankkaart.Kaartnummer);

            tankkaart.ZetKaartnummer("789XYZ12");

            Assert.Equal("789XYZ12", tankkaart.Kaartnummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Test_ZetKaartnummer_invalid(string kaartnummer)
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.Throws<TankkaartException>(() => tankkaart.ZetKaartnummer(kaartnummer));
        }

        [Fact]
        public void Test_ZetPincode_valid()
        {
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", new List<RijbewijsType>());
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder);

            Assert.Equal("1111", tankkaart.Pincode);

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
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", new List<RijbewijsType>());
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31),"1111",bestuurder);

            Assert.Throws<TankkaartException>(() => tankkaart.ZetPincode(pincode));
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_valid()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            var brandstofType = new BrandstofType("Euro95");

            Assert.False(tankkaart.HeeftBrandstofType(brandstofType));

            tankkaart.VoegBrandstofTypeToe(brandstofType);

            Assert.True(tankkaart.HeeftBrandstofType(brandstofType));
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_invalid_brandstofNull()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));

            Assert.Throws<TankkaartException>(() => tankkaart.VoegBrandstofTypeToe(null));
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_invalid_brandstofBestaatAl()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            var diesel = new BrandstofType("Diesel");
            tankkaart.VoegBrandstofTypeToe(diesel);

            Assert.True(tankkaart.HeeftBrandstofType(diesel));
            Assert.Throws<TankkaartException>(() => tankkaart.VoegBrandstofTypeToe(diesel));
        }

        [Fact]
        public void Test_VoegBrandstofTypeToe_invalid_brandstofMetTypeBestaatAl()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            var diesel = new BrandstofType("Diesel");
            tankkaart.VoegBrandstofTypeToe(diesel);
            var diesel2 = new BrandstofType("Diesel");

            Assert.True(tankkaart.HeeftBrandstofType(diesel));
            Assert.Equal(diesel.Type,diesel2.Type);
            Assert.Throws<TankkaartException>(() => tankkaart.VoegBrandstofTypeToe(diesel2));
        }

        [Fact]
        public void Test_VerwijderBrandstofType_valid()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            var brandstofType = new BrandstofType("Diesel");
            tankkaart.VoegBrandstofTypeToe(brandstofType);

            Assert.True(tankkaart.HeeftBrandstofType(brandstofType));

            tankkaart.VerwijderBrandstofType(brandstofType);

            Assert.False(tankkaart.HeeftBrandstofType(brandstofType));
        }

        [Fact]
        public void Test_VerwijderBrandstofType_invalid_brandstofTypeNull()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));

            Assert.Throws<TankkaartException>(() => tankkaart.VerwijderBrandstofType(null));
        }

        [Fact]
        public void Test_VerwijderBrandstofType_invalid_brandstofTypeNietoptankkaart()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            var brandstofType = new BrandstofType("Diesel");

            Assert.False(tankkaart.HeeftBrandstofType(brandstofType));
            Assert.Throws<TankkaartException>(() => tankkaart.VerwijderBrandstofType(brandstofType));
        }

        [Fact]
        public void Test_ZetBestuurder_valid()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", new List<RijbewijsType>());

            Assert.Null(tankkaart.Bestuurder);
            Assert.Null(bestuurder.Tankkaart);

            tankkaart.ZetBestuurder(bestuurder);

            Assert.Equal(bestuurder, tankkaart.Bestuurder);
            Assert.Equal(tankkaart, bestuurder.Tankkaart);
        }

        [Fact]
        public void Test_ZetBestuurder_valid_vorigeBestuurder_tankkaartVerwijderd()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515", new List<RijbewijsType>());
            var bestuurder2 = new Bestuurder("Droesbeke", "Arnout", new DateTime(1995, 01, 21), "99100630516", new List<RijbewijsType>());

            Assert.Null(tankkaart.Bestuurder);
            Assert.Null(bestuurder.Tankkaart);

            tankkaart.ZetBestuurder(bestuurder);
            tankkaart.ZetBestuurder(bestuurder2);

            Assert.Null(bestuurder.Tankkaart);
            Assert.Equal(bestuurder2, tankkaart.Bestuurder);
            Assert.Equal(tankkaart, bestuurder.Tankkaart);
        }

        [Fact]
        public void Test_ZetBestuurder_invalid_bestuurderisnull()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.Throws<TankkaartException>(() => tankkaart.ZetBestuurder(null));
        }


        [Fact]
        public void Test_VerwijderBestuurder_valid()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));
            var bestuurder = new Bestuurder("De Neef","Olivier",new DateTime(1999,10,6),"99100630515",new List<RijbewijsType>());

            tankkaart.ZetBestuurder(bestuurder);

            Assert.Equal(bestuurder, tankkaart.Bestuurder);
            Assert.Equal(tankkaart, bestuurder.Tankkaart);

            tankkaart.VerwijderBestuurder();

            Assert.Null(tankkaart.Bestuurder);
            Assert.Null(bestuurder.Tankkaart);

        }

        [Fact]
        public void Test_VerwijderBestuurder_invalid_bestuurderNull()
        {
            var tankkaart = new Tankkaart(5,"123ABC98",new DateTime(2022,12,31));

            Assert.Null(tankkaart.Bestuurder);
            Assert.Throws<TankkaartException>(() => tankkaart.VerwijderBestuurder());
        }

        [Fact]
        public void Test_BlokkeerKaart_valid()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.False(tankkaart.IsGeblokkeerd);

            tankkaart.BlokkeerKaart();

            Assert.True(tankkaart.IsGeblokkeerd);
        }

        [Fact]
        public void Test_BlokkeerKaart_invalid()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            tankkaart.BlokkeerKaart();

            Assert.True(tankkaart.IsGeblokkeerd);
            Assert.Throws<TankkaartException>(() => tankkaart.BlokkeerKaart());
        }

        [Fact]
        public void Test_DeblokkeerKaart_valid()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            tankkaart.BlokkeerKaart();

            Assert.True(tankkaart.IsGeblokkeerd);

            tankkaart.DeblokkeerKaart();

            Assert.False(tankkaart.IsGeblokkeerd);
        }

        [Fact]
        public void Test_DeblokkeerKaart_invalid()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            Assert.False(tankkaart.IsGeblokkeerd);
            Assert.Throws<TankkaartException>(() => tankkaart.DeblokkeerKaart());
        }

        [Fact()]
        public void Test_HeeftBrandstofType()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            var brandstof = new BrandstofType("Benzine");

            Assert.False(tankkaart.HeeftBrandstofType(brandstof));

            tankkaart.VoegBrandstofTypeToe(brandstof);

            Assert.True(tankkaart.HeeftBrandstofType(brandstof));
        }

        [Fact]
        public void Test_HeefBranstofTypeNull()
        {
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
          
            Assert.ThrowsAny<TankkaartException>(() => tankkaart.HeeftBrandstofType(null));

        }
        [Fact]
        public void IsGearchiveerdTrue()
        {
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",
                new List<RijbewijsType>());
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));
            
            tankkaart.ZetBestuurder(bestuurder);

            Assert.False(tankkaart.IsGearchiveerd);
            Assert.Equal(bestuurder,tankkaart.Bestuurder);
            Assert.Equal(tankkaart,bestuurder.Tankkaart);

            tankkaart.ZetGearchiveerd(true);
            Assert.True(tankkaart.IsGearchiveerd);
            Assert.Null(tankkaart.Bestuurder);
            Assert.Null(bestuurder.Tankkaart);
        }

        [Fact()]
        public void ZetIsGearchiveerdFalse()
        {
            var bestuurder = new Bestuurder("De Neef", "Olivier", new DateTime(1999, 10, 6), "99100630515",
                new List<RijbewijsType>());
            var tankkaart = new Tankkaart(5, "123ABC98", new DateTime(2022, 12, 31));

            tankkaart.ZetBestuurder(bestuurder);
            tankkaart.ZetGearchiveerd(true);

            Assert.True(tankkaart.IsGearchiveerd);
            Assert.Null(tankkaart.Bestuurder);
            Assert.Null(bestuurder.Tankkaart);

            tankkaart.ZetGearchiveerd(false);

            Assert.False(tankkaart.IsGearchiveerd);

        }
    }

    }
