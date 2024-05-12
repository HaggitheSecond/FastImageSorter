using FastImageSorter.UI.Common;
using FastImageSorter.UI.MVVM;

namespace FastImageSorter.UI.UI.Results;

public class SortingResultViewModel : WizardEndPageViewModel<SortingRun>
{
    private SortingRun _run;

    public SortingRun Run
    {
        get { return this._run; }
        set { this.SetProperty(ref this._run, value, () => this.Run); }
    }

    public override WizardPageButton GetNext()
    {
        return new WizardPageButton("Finish", async () => {  }, () => { return true; });
    }

    public override WizardPageButton GetPrevious()
    {
        return null;
    }

    public override void SetData(SortingRun data)
    {
        this.Run = data;
    }
}
