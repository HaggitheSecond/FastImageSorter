using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using FastImageSorter.UI.Helpers;
using FastImageSorter.UI.MVVM;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace FastImageSorter.UI.UI.Sorting;

public class SortingViewModel : WizardPageViewModel<SortingRun, SortingRun>
{
    private int _totalImageCount;
    private int _finishedImageCount;
    private bool _finishedSorting;
    private ObservableCollection<SortingBucketItemViewModel> _unsortedItems;
    private SortingBucketItemViewModel? _selectedUnsortedItem;

    private SortingRun _sortingRun;

    public int TotalImageCount
    {
        get { return this._totalImageCount; }
        set { this.SetProperty(ref this._totalImageCount, value, () => this.TotalImageCount); }
    }

    public int FinishedImageCount
    {
        get { return this._finishedImageCount; }
        set { this.SetProperty(ref this._finishedImageCount, value, () => this.FinishedImageCount); }
    }

    public bool FinishedSorting
    {
        get { return this._finishedSorting; }
        set { this.SetProperty(ref this._finishedSorting, value, () => this.FinishedSorting); }
    }

    public ObservableCollection<SortingBucketItemViewModel> UnsortedItems
    {
        get { return this._unsortedItems; }
        set { this.SetProperty(ref this._unsortedItems, value, () => this.UnsortedItems); }
    }

    public SortingBucketItemViewModel? SelectedUnsortedItem
    {
        get { return this._selectedUnsortedItem; }
        set
        {

            this.SelectedUnsortedItem?.Deactivate();
            value?.Activate();

            this.SetProperty(ref this._selectedUnsortedItem, value, () => this.SelectedUnsortedItem);
        }
    }

    public SortingRun SortingRun
    {
        get { return this._sortingRun; }
        set { this.SetProperty(ref this._sortingRun, value, () => this.SortingRun); }
    }

    public SortingViewModel()
        : base("Finish", "Cancel")
    {

    }

    public override void SetData(SortingRun data)
    {
        this.SortingRun = data;

        var files = DirectoryHelper.GetImageFiles(data.SourceDirectoryPath);

        this.TotalImageCount = files.Count();
        this.FinishedImageCount = 0;

        this.UnsortedItems = new ObservableCollection<SortingBucketItemViewModel>(files.Select(f => new SortingBucketItemViewModel(new FileInfo(f))));
        this.SelectedUnsortedItem = this.UnsortedItems.First();
    }

    public override SortingRun GetData()
    {
        return this.SortingRun;
    }

    public void SortItem(Key key)
    {
        if (this.UnsortedItems.Count == 0)
            return;

        var bucket = this.SortingRun.Buckets.FirstOrDefault(b => b.Action.Key == key);

        if (bucket != null)
        {
            if (this.SelectedUnsortedItem == null)
                return;

            bucket.Items.Add(this.SelectedUnsortedItem.ToItem());

            this.UnsortedItems.RemoveAt(0);

            if (this.UnsortedItems.Count == 0)
                this.SelectedUnsortedItem = null;
            else
                this.SelectedUnsortedItem = this.UnsortedItems.First();

            this.SelectedUnsortedItem?.Activate();

            this.FinishedImageCount++;

            this.FinishedSorting = this.UnsortedItems.Count == 0;
        }
    }
}
