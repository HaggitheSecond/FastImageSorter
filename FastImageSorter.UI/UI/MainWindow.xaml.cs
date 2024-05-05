using FastImageSorter.UI.UI.Sorting;
using System.Windows;

namespace FastImageSorter.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.Activate();

        this.SetSettingsView();
    }

    public void SetSettingsView()
    {
        this.MainContentGrid.Children.Clear();

        var viewModel = new SortingSettingsViewModel();
        viewModel.SettingsAcceptedEvent += (o, e) => this.SetSortingView(viewModel);

        this.MainContentGrid.Children.Add(new SortingSettingsView()
        {
            DataContext = viewModel
        });
    }

    public void SetSortingView(SortingSettingsViewModel sortingSettingsViewModel)
    {
        this.MainContentGrid.Children.Clear();

        var viewModel = new SortingViewModel(sortingSettingsViewModel);
        viewModel.FinishedSortingEvent += (o, e) => this.SetSettingsView();

        this.MainContentGrid.Children.Add(new SortingView()
        {
            DataContext = viewModel,
        });
    }
}