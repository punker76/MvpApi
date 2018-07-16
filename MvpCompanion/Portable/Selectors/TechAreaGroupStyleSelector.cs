using Telerik.XamarinForms.DataGrid;
using Xamarin.Forms;

namespace MvpCompanion.Portable.Selectors
{
    internal class TechAreaGroupStyleSelector : DataGridStyleSelector
    {
        public DataGridStyle TechnologyAreaGroupStyle { get; set; }
        
        public override DataGridStyle SelectStyle(object item, BindableObject container)
        {
            return TechnologyAreaGroupStyle;

            //return base.SelectStyle(item, container);
        }
    }
}
