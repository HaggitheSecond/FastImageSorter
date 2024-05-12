using DevExpress.Mvvm;
using FastImageSorter.UI.Common;

namespace FastImageSorter.UI.UI.Run;
public class SortingRunBucketItemViewModel : ViewModelBase
{
	private BucketItem _item;

	public BucketItem Item
	{
		get { return this._item; }
		set { this.SetProperty(ref this._item, value, () => this.Item); }
	}

    public SortingRunBucketItemViewModel(BucketItem item)
    {
        this.Item = item;
    }
}
