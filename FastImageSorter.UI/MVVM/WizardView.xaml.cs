using System.Windows.Controls;

namespace FastImageSorter.UI.MVVM;

public partial class WizardView : UserControl
{
    private List<WizardPageViewModel> _pages;

    public WizardView(List<WizardPageViewModel> pages)
    {
        this.InitializeComponent();

        this._pages = pages;
    }

    public void Activate()
    {
        var page = this._pages.First();

        this.NextButton.Command = page.NextCommand;
        this.PreviousButton.Command = page.PreviousCommand;

        page.Activate();
    }

    private UserControl GetView(WizardPageViewModel vm)
    {
        var viewType = vm.GetType().Assembly.GetTypes().FirstOrDefault(f => f.Name.Contains(vm.GetType().Name.Replace("ViewModel", "")));

        if (viewType is null)
            throw new Exception("Could not find view for " + vm.GetType().Name);

        var view = Activator.CreateInstance(viewType);

        if (view is UserControl userControl)
            return userControl;

        throw new Exception("Bad view type for view " + viewType.Name);
    }
}
