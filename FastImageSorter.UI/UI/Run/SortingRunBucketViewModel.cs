using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using System.Collections.ObjectModel;

namespace FastImageSorter.UI.UI.Run;

public class SortingRunBucketViewModel : ViewModelBase
{
	private Bucket _bucket;

    private SortingRunBucketItemViewModel _currentItem;
    private ObservableCollection<SortingRunBucketItemViewModel> _items;

    public Bucket Bucket
	{
		get { return this._bucket; }
		set { this.SetProperty(ref this._bucket, value, () => this.Bucket); }
	}

	public SortingRunBucketItemViewModel CurrentItem
	{
		get { return this._currentItem; }
		set { this.SetProperty(ref this._currentItem, value, () => this.CurrentItem); }
	}

	public ObservableCollection<SortingRunBucketItemViewModel> Items
	{
		get { return this._items; }
		set { this.SetProperty(ref this._items, value, () => this.Items); }
	}

	public SortingRunBucketViewModel(Bucket bucket)
    {
		this.Bucket = bucket;
		this.Items = new ObservableCollection<SortingRunBucketItemViewModel>(bucket.Items.Select(f => new SortingRunBucketItemViewModel(f)));
    }
}
