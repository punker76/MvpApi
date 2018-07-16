using System.Collections.ObjectModel;
using MvpCompanion.Portable.Common;

namespace MvpCompanion.Portable.ViewModels
{
    public class RootPageMasterViewModel
    {
        public ObservableCollection<RootPageMenuItem> MenuItems { get; set; }

        public RootPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<RootPageMenuItem>
            {
                new RootPageMenuItem {Id = 0, Title = "Page 1"},
                new RootPageMenuItem {Id = 1, Title = "Page 2"},
                new RootPageMenuItem {Id = 2, Title = "Page 3"},
                new RootPageMenuItem {Id = 3, Title = "Page 4"},
                new RootPageMenuItem {Id = 4, Title = "Page 5"}
            };
        }
    }
}
