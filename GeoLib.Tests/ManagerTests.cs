using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit;
using GeoLib.Data;
using GeoLib.Contracts;
using GeoLib.Services;
using FluentAssertions;

namespace GeoLib.Tests
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void test_zip_code_retrieval()
        {
            Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();

            ZipCode zipCode = new ZipCode()
            {
                City = "LINCOLN PARK",
                State = new State() {Abbreviation = "NJ"},
                Zip = "07035"
            };

            mockZipCodeRepository.Setup(obj => obj.GetByZip("07035")).Returns(zipCode);

            IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);

            ZipCodeData data = geoService.GetZipCodeData("07035");

            data.City.ToUpper().Should().Be("LINCOLN PARK");
            data.State.Should().Be("NJ");
        }
    }
}
