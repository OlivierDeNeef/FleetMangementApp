﻿using System.Runtime.InteropServices;
using DomainLayer.Exceptions.Models;
using DomainLayer.Models;
using Xunit;

namespace DomainLayerTests.Models
{
    /// <summary>
    /// Todo : ZetNummerplaat uitgebreider testen// Done
    /// ASK : Rekening houden met gepersonaliseerde nummerplaten? (bv enkel letters)
    /// Todo : ZetBestuurder uitgebreider testen //Dome
    /// Todo : Tests schrijven voor verwijder bestuurder//Dome
    /// Todo : Ook testen op null waarde//Dome
    /// </summary>


    public class VoertuigTests
    {
        private readonly Voertuig _voertuig;

      
        public VoertuigTests()
        {
            _voertuig = new Voertuig();
        } 
        [Fact()]
        public void ZetIdTest()
        {
            _voertuig.ZetId(4);
            Assert.Equal(4, _voertuig.Id);

        }

        [Fact()]
        public void ZetId_NegatieveId_GooitException()
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetId(-4));
        }

        [Fact()]
        public void ZetMerkTestGeldig()
        {
           _voertuig.ZetMerk("Mercedez");
           Assert.Equal("Mercedez", _voertuig.Merk);
        }

         [Theory]
         [InlineData("  ")]
         [InlineData(null)]
        public void ZetMerkOngeldig(string merk)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetMerk(merk));
        }

        [Fact()]
        public void ZetModelGeldig()
        {
            _voertuig.ZetModel("A-Klasse");
            Assert.Equal("A-Klasse", _voertuig.Model);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData(null)]
        public void ZetModelOngeldig(string model)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetModel(model));
        }

        [Fact()]
        public void ZetChassisnummerTest()
        {
            _voertuig.ZetChassisnummer("123456ABCDEF789GH");
            Assert.Equal("123456ABCDEF789GH", _voertuig.Chassisnummer);
        }

        [Theory]
        [InlineData("123456ABCDEF78")]
        [InlineData("")]
        [InlineData(null)]
        public void ZetChassisnummerInValid(string nummer)
        {
            
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetChassisnummer(nummer));
        }

        [Fact()]
        public void ZetWagenTypeTest()
        {
            WagenType w = new WagenType();
            w.Id = 1;
            w.Type = "Bestelbus";
            _voertuig.ZetWagenType(w);

            Assert.Equal(1, _voertuig.WagenType.Id);
            Assert.Equal("Bestelbus", _voertuig.WagenType.Type);
        }

        [Fact()]
        public void ZetWagenTypeNullTest()
        {
            WagenType w = null;
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetWagenType(w));

        }

        [Theory]
        [InlineData(0, "Bestelbus")]
        [InlineData(1, "")]
        public void ZetWagenTypeIdInvalidAndInvalidType(int id, string type)
        {
            WagenType w = new WagenType();
            w.Id = id;
            w.Type = type;
           
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetWagenType(w));

        }
        
        [Fact()]
        public void ZetBrandstofTypeNull()
        {
         
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetBrandstofType(null));
        }
        [Fact()]
        public void ZetBrandstofTypeTest()
        {
            BrandstofType b = new BrandstofType("Benzine");
            b.ZetId(1);
            _voertuig.SetBrandstofType(b);

            Assert.Equal(1, _voertuig.BrandstofType.Id);
            Assert.Equal("BENZINE", _voertuig.BrandstofType.Type);
        }

        [Theory]
        [InlineData("1-123-DEF")]
        [InlineData("123-ABC")]
        public void ZetNummerplaatTest(string plaat)
        {
            _voertuig.ZetNummerplaat(plaat);
            Assert.Equal(plaat, _voertuig.Nummerplaat);
        }

        [Theory]
        [InlineData("12-CD")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ZetNummerplaatInvalid(string plaat)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetNummerplaat(plaat));
        }
        [Fact()]
        public void ZetKleurTest()
        {
            _voertuig.ZetKleur("Rood");
            Assert.Equal("Rood", _voertuig.Kleur);

        }

        [Fact()]
        public void ZetAantalDeurenTest()
        {
            _voertuig.ZetAantalDeuren(4);
            Assert.Equal(4, _voertuig.AantalDeuren);

        }

        [Theory]
        [InlineData(2)]
        [InlineData(7)]
        public void ZetAantalDeurenOngeldig(int aantal)
        {
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetAantalDeuren(aantal));
        }
        [Fact()]
        public void ZetBestuurderTestGeldig() // TODO: Olivier
        {
            Bestuurder b = new Bestuurder();
            _voertuig.ZetBestuurder(b);

            Assert.Equal(b, _voertuig.Bestuurder);
            
        }

        [Fact()]
        
        public void ZetBestuurderTestOngeldig()
        {
            Bestuurder b = new Bestuurder();
            _voertuig.ZetBestuurder(b);
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.ZetBestuurder(b));
        }



        [Fact()]
        public void VerwijderBestuurder()
        {
            Bestuurder b = new Bestuurder();
            _voertuig.ZetBestuurder(b);
            _voertuig.VerwijderBestuurder();
            Assert.Null(_voertuig.Bestuurder);
        }


        [Fact()]
        public void VerwijderBestuurderIsNull(){
           
            Assert.ThrowsAny<VoertuigException>(() => _voertuig.VerwijderBestuurder());

        }

        [Fact()]
        public void ZetIsGearchiveerdGeslaagd()
        {
            _voertuig.ZetGearchiveerd(true);
            Assert.True(_voertuig.IsGearchiveerd);
        }
        [Fact()]
        public void ZetIsGearchiveerdGefaald()
        {
            _voertuig.ZetGearchiveerd(false);
            Assert.False(_voertuig.IsGearchiveerd);
        }
    }
}