using System.Collections.Generic;

namespace AgencyService
{
    public static class AgencyApiService
    {
        public static IEnumerable<Property> GetDatabaseProperties()
        {
            return new[]
            {
                GetValidPropertyForOtbreAgency(),
                GetValidPropertyFromCreAgency(),
                GetValidPropertyFromLreAgency(),
            };
        }

        public static Property GetValidPropertyForOtbreAgency()
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

        public static Property GetValidPropertyFromCreAgency()
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

        public static Property GetValidPropertyFromLreAgency()
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
    }
}
