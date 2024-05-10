using DevExpress.Mvvm;
using FastImageSorter.UI.UI.Sorting;
using System.Collections.ObjectModel;

namespace FastImageSorter.UI.UI
{
    public class BucketResultViewModel : ViewModelBase
    {
        private ObservableCollection<BucketItemResultViewModel> _results;
        private BucketViewModel _bucket;

        public ObservableCollection<BucketItemResultViewModel> Results
        {
            get { return this._results; }
            set { this.SetProperty(ref this._results, value, () => this.Results); }
        }

        public BucketViewModel Bucket
        {
            get { return this._bucket; }
            set { this.SetProperty(ref this._bucket, value, () => this.Bucket); }
        }

        public BucketResultViewModel()
        {
            this.Results = new ObservableCollection<BucketItemResultViewModel>();
        }
    }
}
