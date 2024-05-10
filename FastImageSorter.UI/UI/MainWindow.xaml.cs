using FastImageSorter.UI.MVVM;
using FastImageSorter.UI.UI;
using FastImageSorter.UI.UI.Settings;
using System.Windows;
using System.Windows.Controls;

namespace FastImageSorter.UI;

public partial class MainWindow : Window
{
    public List<(UserControl view, WizardPageViewModel viewModel)> Pages { get; set; }

    public MainWindow()
    {
        this.InitializeComponent();
        this.Activate();

        this.Pages = new List<(UserControl view, WizardPageViewModel viewModel)>
        {
            (new SortingSettingsView(), new SortingSettingsViewModel()),

        };
    }
}