using System.Windows.Controls;

namespace FastImageSorter.UI.UI.Settings
{
    public partial class SortingSettingsView : UserControl
    {
        public SortingSettingsView()
        {
            this.InitializeComponent();
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (((SortingSettingsBucketViewModel)e.Row.Item).CanBeEdited == false)
            {
                e.Cancel = true;
            }
        }
    }
}
