using Xunit;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Exceptions.Models;

namespace DomainLayerTests.Models
{
    public class RijbewijsTypeTests
    {
        private readonly RijbewijsType _rijbewijsType;

        public RijbewijsTypeTests()
        {
            _rijbewijsType = new RijbewijsType("B");
        }

        [Fact()]
        public void ZetIdValid()
        {
            _rijbewijsType.ZetId(5);
            Assert.Equal(5, _rijbewijsType.Id);
        }

        [Fact()]
        public void ZetTypeValid()
        {
            _rijbewijsType.ZetType("A");
            Assert.Equal("A", _rijbewijsType.Type);
        }

        [Fact()]
        public void ZetIdInvalid()
        {
            Assert.ThrowsAny<RijbewijsTypeException>(() => _rijbewijsType.ZetId(-2));
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("B")]

        public void ZetTypeInvalid(string type)
        {
            Assert.ThrowsAny<RijbewijsTypeException>(() => _rijbewijsType.ZetType(type));
        }


    }   
}