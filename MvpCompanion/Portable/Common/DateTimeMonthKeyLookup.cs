using MvpApi.Common.Models;
using MvpApi.Common.ServiceModels;
using Telerik.XamarinForms.Common.Data;

namespace MvpCompanion.Portable.Common
{
    public class DateTimeMonthKeyLookup : IKeyLookup
    {
        public object GetKey(object instance)
        {
            return (instance as ContributionsModel)?.StartDate?.Date;
        }
    }
}
