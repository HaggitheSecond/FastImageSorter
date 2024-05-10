using DevExpress.Mvvm;
using FastImageSorter.UI.Common;

namespace FastImageSorter.UI.UI.Run;

public class SortingRunBucketViewModel : ViewModelBase
{
	private Bucket _bucket;

	public Bucket Bucket
	{
		get { return this._bucket; }
		set { this.SetProperty(ref this._bucket, value, () => this.Bucket); }
	}


	public SortingRunBucketViewModel(Bucket bucket)
    {
		this.Bucket = bucket;
    }
}
