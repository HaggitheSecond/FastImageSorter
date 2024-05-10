using FastImageSorter.UI.UI.Run;
using System.Windows;

namespace FastImageSorter.UI.UI.Run;

public partial class SortingRunView
{
    public SortingRunViewModel? ViewModel => this.DataContext as SortingRunViewModel;

    public SortingRunView()
    {
        this.InitializeComponent();

        this.DataContextChanged += this.SortingRunView_DataContextChanged;
    }

    private void SortingRunView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (this.ViewModel is null)
            return;

        this.ViewModel.CloseRequested += (o, i) => { this.DialogResult = i; this.Close(); };
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (this.ViewModel != null)
        {
            await this.ViewModel.ExecuteSorting();
        }
    }
}
