using System.Windows.Controls;
using System.Windows.Input;

namespace FastImageSorter.UI.UI.Sorting;

public partial class SortingView : UserControl
{
    public SortingViewModel? ViewModel => this.DataContext as SortingViewModel;

    public SortingView()
    {
        this.InitializeComponent();

        this.MainImage.Focusable = true;

        this.Loaded += (sender, args) =>
        {
            this.MainImage.Focus();
            Keyboard.Focus(this.MainImage);
        };
    }

    private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        this.ViewModel?.SortItem(e.Key);
    }
}
