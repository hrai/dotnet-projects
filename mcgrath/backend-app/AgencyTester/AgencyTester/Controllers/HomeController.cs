using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgencyService;

namespace AgencyTester.Controllers
{
    public class HomeController : Controller
    {
        private IPropertyMatcher _propertyMatcher;

        public HomeController(IPropertyMatcher propertyMatcher)
        {
            _propertyMatcher = propertyMatcher;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Submit(Property agencyProperty)
        {
            if (string.IsNullOrWhiteSpace(agencyProperty.Name) ||
                string.IsNullOrWhiteSpace(agencyProperty.Address) ||
                string.IsNullOrWhiteSpace(agencyProperty.AgencyCode))
                return "Invalid input";

            var databaseProperties = AgencyApiService.GetDatabaseProperties();
            if (databaseProperties.Any(databaseProperty => _propertyMatcher.IsMatch(agencyProperty, databaseProperty)))
            {
                return "A match was found";
            }

            return "No match was found";
        }

    }
}