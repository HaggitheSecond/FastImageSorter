using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using FastImageSorter.UI.MVVM;
using FastImageSorter.UI.UI.Sorting;
using System.Collections.ObjectModel;

namespace FastImageSorter.UI.UI.Run;

public class SortingRunViewModel : WizardPageViewModel<SortingRun, SortingRun>
{
    private ObservableCollection<SortingRunBucketViewModel> _buckets;
    private SortingRunBucketViewModel _currentBucket;
    private SortingRunBucketItemViewModel _currentItem;

    private int _totalBucketCount;
    private int _totalItemCount;

    private int _finishedBucketCount;
    private int _finishedItemCount;

    private SortingRun _sortingRun;

    public ObservableCollection<SortingRunBucketViewModel> Buckets
    {
        get { return this._buckets; }
        set { this.SetProperty(ref this._buckets, value, () => this.Buckets); }
    }

    public SortingRunBucketViewModel CurrentBucket
    {
        get { return this._currentBucket; }
        set { this.SetProperty(ref this._currentBucket, value, () => this.CurrentBucket); }
    }

    public SortingRunBucketItemViewModel CurrentItem
    {
        get { return this._currentItem; }
        set { this.SetProperty(ref this._currentItem, value, () => this.CurrentItem); }
    }

    public int TotalBucketCount
    {
        get { return this._totalBucketCount; }
        set { this.SetProperty(ref this._totalBucketCount, value, () => this.TotalBucketCount); }
    }

    public int FinishedBucketCount
    {
        get { return this._finishedBucketCount; }
        set { this.SetProperty(ref this._finishedBucketCount, value, () => this.FinishedBucketCount); }
    }

    public int TotalItemCount
    {
        get { return this._totalItemCount; }
        set { this.SetProperty(ref this._totalItemCount, value, () => this.TotalItemCount); }
    }

    public int FinishedItemCount
    {
        get { return this._finishedItemCount; }
        set { this.SetProperty(ref this._finishedItemCount, value, () => this.FinishedItemCount); }
    }

    public SortingRun SortingRun
    {
        get { return this._sortingRun; }
        set { this.SetProperty(ref this._sortingRun, value, () => this.SortingRun); }
    }

    public DelegateCommand CloseCommand { get; set; }
    public DelegateCommand CancelCommand { get; set; }

    public event EventHandler<bool> CloseRequested;

    public SortingRunViewModel()
    {

    }

    public override void SetData(SortingRun data)
    {
        this.SortingRun = data;

        this.Buckets = new ObservableCollection<SortingRunBucketViewModel>(data.Buckets.Select(f => new SortingRunBucketViewModel(f)));
    }

    public override SortingRun GetData()
    {
        return this.SortingRun;
    }
}
