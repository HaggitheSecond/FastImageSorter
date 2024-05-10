using System.Windows.Controls;

namespace FastImageSorter.UI.UI.Sorting
{
    public partial class SortingSettingsView : UserControl
    {
        public SortingSettingsView()
        {
            this.InitializeComponent();
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (((BucketViewModel)e.Row.Item).CanBeEdited == false)
            {
                e.Cancel = true;
            }
        }
    }
}
