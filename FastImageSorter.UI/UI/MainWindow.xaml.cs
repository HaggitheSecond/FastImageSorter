using FastImageSorter.UI.MVVM;
using FastImageSorter.UI.UI;
using FastImageSorter.UI.UI.Results;
using FastImageSorter.UI.UI.Run;
using FastImageSorter.UI.UI.Settings;
using FastImageSorter.UI.UI.Sorting;
using System.Windows;
using System.Windows.Controls;

namespace FastImageSorter.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.Activate();

        var wizard = new WizardView(
        [
            new SortingSettingsViewModel(),
            new SortingViewModel(),
            new SortingRunViewModel(),
            new SortingRunViewModel()
        ]);

        this.MainContentGrid.Content = wizard;
        wizard.Activate();
    }
}