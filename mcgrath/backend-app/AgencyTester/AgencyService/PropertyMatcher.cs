using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AgencyService
{
    public class PropertyMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            if (agencyProperty == null || databaseProperty == null)
                return false;

            switch (agencyProperty.AgencyCode.ToUpperInvariant())
            {
                case "OTBRE":
                    {
                        var specialCharacterRegex = new Regex(@"([^A-Za-z])|(\s+)");
                        var cleanedAgencyPropertyName = specialCharacterRegex.Replace(agencyProperty.Name, " ");
                        var cleanedDatabasePropertyName = specialCharacterRegex.Replace(databaseProperty.Name, " ");
                        var cleanedAgencyPropertyNameArray = GetStringArray(cleanedAgencyPropertyName);

                        var a = cleanedAgencyPropertyNameArray.Intersect(GetStringArray(cleanedDatabasePropertyName)).ToList();
                        return a.Count() == cleanedAgencyPropertyNameArray.Count();
                    }
                case "LRE":
                {
                    var latitudeDifference = agencyProperty.Latitude - databaseProperty.Latitude;
                    var longitudeDifference = agencyProperty.Longitude - databaseProperty.Longitude;

                    if (ConvertDegreesToMetres(latitudeDifference) > 200)
                        return false;

                    if (ConvertDegreesToMetres(longitudeDifference) > 200)
                        return false;

                    return true;
                }
                case "CRE":
                    {
                        var agencyPropertyName = GetStringArray(agencyProperty.Name).Reverse().ToList();
                        return agencyPropertyName.Intersect(GetStringArray(databaseProperty.Name)).Count() == agencyPropertyName.Count();
                    }
                default:
                    return false;

            }
        }

        private decimal ConvertDegreesToMetres(decimal degree)
        {
            return Math.Abs(degree) * 111 * 1000;
        }

        private IEnumerable<string> GetStringArray(string stringToSplit)
        {
            return stringToSplit.ToLowerInvariant().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
