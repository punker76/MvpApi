using MvpApi.Common.Models;
using MvpApi.Common.ServiceModels;
using Telerik.XamarinForms.Common.Data;

namespace MvpCompanion.Portable.Common
{
    public class TechnologyKeyLookup : IKeyLookup
    {
        public object GetKey(object instance)
        {
            return (instance as ContributionsModel)?.ContributionTechnology.Name;
        }
    }
}
