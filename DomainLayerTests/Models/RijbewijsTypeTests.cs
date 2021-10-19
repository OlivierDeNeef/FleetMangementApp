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

namespace DomainLayer.Models.Tests
{
    public class RijbewijsTypeTests
    {
        private RijbewijsType _rijbewijsType;

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
            Assert.ThrowsAny<RijbewijsTypeManagerException>(() => _rijbewijsType.ZetId(-2));
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void ZetTypeInvalid(string type)
        {
            Assert.ThrowsAny<RijbewijsTypeManagerException>(() => _rijbewijsType.ZetType(type));
        }


    }   
}