using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyService
{
    public interface IPropertyMatcher
    {
        bool IsMatch(Property agencyProperty, Property databaseProperty);
    }
}
