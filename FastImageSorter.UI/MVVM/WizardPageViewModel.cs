using DevExpress.Mvvm;

namespace FastImageSorter.UI.MVVM;

public abstract class WizardPageViewModel : ViewModelBase
{
    private string _nextButtonTitle;
    private string _previousButtonTitle;

    public string NextButtonTitle
    {
        get { return this._nextButtonTitle; }
        set { this.SetProperty(ref this._nextButtonTitle, value, () => this.NextButtonTitle); }
    }

    public string PreviousButtonTitle
    {
        get { return this._previousButtonTitle; }
        set { this.SetProperty(ref this._previousButtonTitle, value, () => this.PreviousButtonTitle); }
    }

    public AsyncCommand NextCommand { get; }
    public AsyncCommand PreviousCommand { get; }

    public abstract WizardPageButton GetNext();
    public abstract WizardPageButton GetPrevious();
}


public record WizardPageButton(string InitalTitle, Func<Task> Execute, Func<bool> CanExecute);

public abstract class WizardStartPageViewModel<T> : WizardPageViewModel
{
    public abstract T GetData();
}

public abstract class WizardPageViewModel<T, G> : WizardPageViewModel
{
	public abstract void SetData(T data);

	public abstract G GetData();
}

public abstract class WizardEndPageViewModel<T> : WizardPageViewModel
{
    public abstract T GetData();
}
