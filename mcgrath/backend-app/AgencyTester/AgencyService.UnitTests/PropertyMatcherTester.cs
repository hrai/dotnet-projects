﻿using NUnit.Framework;
using FluentAssertions;

namespace AgencyService.UnitTests
{
    [TestFixture]
    public class PropertyMatcherTester
    {
        private PropertyMatcher _propertyMatcher;

        [SetUp]
        public void Setup()
        {
            _propertyMatcher = new PropertyMatcher();
        }

        [Test]
        public void Otbre_IsMatch_ReturnsTrue_WhenNameMatches()
        {
            var validProperty = GetValidPropertyForOtbreAgency();

            var validPropertyWithMessyDetails = new Property
            {
                AgencyCode = "OTBRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "*Super*-High! APARTMENT (Sydney)",
                Latitude = 23,
                Longitude = 34,
            };

            _propertyMatcher.IsMatch(validProperty, validPropertyWithMessyDetails).Should().BeTrue();
        }

        [Test]
        public void Otbre_IsMatch_ReturnsFalse_WhenNameDoesntMatch()
        {
            var validProperty = GetValidPropertyForOtbreAgency();

            var validPropertyWithMessyDetails = new Property
            {
                AgencyCode = "OTBRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "*Super*-High! APARTMENTS (Sydney)",
                Latitude = 23,
                Longitude = 34,
            };

            _propertyMatcher.IsMatch(validProperty, validPropertyWithMessyDetails).Should().BeFalse();
        }

        private static Property GetValidPropertyForOtbreAgency()
        {
            var validProperty = new Property
            {
                AgencyCode = "OTBRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "Super High Apartment Sydney",
                Latitude = 23,
                Longitude = 34,
            };
            return validProperty;
        }

        [Test]
        public void Cre_IsMatch_ReturnsTrue_WhenNameMatches()
        {
            var validProperty = GetValidPropertyFromCreAgency();

            var validPropertyWithMessyDetails = new Property
            {
                AgencyCode = "CRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "Apartments Summit The",
                Latitude = 23,
                Longitude = 34,
            };

            _propertyMatcher.IsMatch(validProperty, validPropertyWithMessyDetails).Should().BeTrue();
        }

        [Test]
        public void Cre_IsMatch_ReturnsFalse_WhenNameDoesntMatches()
        {
            var validProperty = GetValidPropertyFromCreAgency();

            var invalidPropertyWithMessyDetails = new Property
            {
                AgencyCode = "CRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "Apartments Summit Their",
                Latitude = 23,
                Longitude = 34,
            };

            _propertyMatcher.IsMatch(validProperty, invalidPropertyWithMessyDetails).Should().BeFalse();
        }

        private Property GetValidPropertyFromCreAgency()
        {
            var validProperty = new Property
            {
                AgencyCode = "CRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "The Summit Apartments",
                Latitude = 23,
                Longitude = 34,
            };
            return validProperty;
        }

        private Property GetValidPropertyFromLreAgency()
        {
            var validProperty = new Property
            {
                AgencyCode = "LRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "The Summit Apartments",
                Latitude = 23.02m,
                Longitude = 34,
            };
            return validProperty;
        }

        [Test]
        public void Lre_IsMatch_ReturnsTrue_WhenLatitudeLocationIsWithinRange()
        {
            var validProperty = GetValidPropertyFromLreAgency();

            var validPropertyWithMessyDetails = new Property
            {
                AgencyCode = "LRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "Apartments Summit The",
                Latitude = 23.019m,
                Longitude = 34.001m,
            };

            _propertyMatcher.IsMatch(validProperty, validPropertyWithMessyDetails).Should().BeTrue();
        }

        [Test]
        public void Lre_IsMatch_ReturnsTrue_WhenLongitudeLocationIsWithinRange()
        {
            var validProperty = GetValidPropertyFromLreAgency();

            var validPropertyWithMessyDetails = new Property
            {
                AgencyCode = "LRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "Apartments Summit The",
                Latitude = 23.019m,
                Longitude = 34.001m,
            };

            _propertyMatcher.IsMatch(validProperty, validPropertyWithMessyDetails).Should().BeTrue();
        }

        [Test]
        public void Lre_IsMatch_ReturnsFalse_WhenLatitudeLocationIsOutsideRange()
        {
            var validProperty = GetValidPropertyFromLreAgency();

            var validPropertyWithMessyDetails = new Property
            {
                AgencyCode = "LRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "Apartments Summit The",
                Latitude = 35,
                Longitude = 23.019m,
            };

            _propertyMatcher.IsMatch(validProperty, validPropertyWithMessyDetails).Should().BeFalse();
        }

        [Test]
        public void Lre_IsMatch_ReturnsFalse_WhenLongitudeLocationIsOutsideRange()
        {
            var validProperty = GetValidPropertyFromLreAgency();

            var validPropertyWithMessyDetails = new Property
            {
                AgencyCode = "LRE",
                Address = "32 Sir John Young Crescent, Sydney NSW",
                Name = "Apartments Summit The",
                Latitude = 23.019m,
                Longitude = 39.019m,
            };

            _propertyMatcher.IsMatch(validProperty, validPropertyWithMessyDetails).Should().BeFalse();
        }
    }
}
