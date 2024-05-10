using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastImageSorter.UI.UI.Sorting
{
    public class SortingRunViewModel : ViewModelBase
    {
        private ObservableCollection<BucketViewModel> _buckets;
        private BucketViewModel _currentBucket;
        private BucketItemViewModel _currentItem;

        private int _totalBucketCount;
        private int _totalItemCount;

        private int _finishedBucketCount;
        private int _finishedItemCount;
        private ObservableCollection<BucketResultViewModel> _results;

        public ObservableCollection<BucketViewModel> Buckets
        {
            get { return this._buckets; }
            set { this.SetProperty(ref this._buckets, value, () => this.Buckets); }
        }

        public BucketViewModel CurrentBucket
        {
            get { return this._currentBucket; }
            set { this.SetProperty(ref this._currentBucket, value, () => this.CurrentBucket); }
        }

        public BucketItemViewModel CurrentItem
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

        public ObservableCollection<BucketResultViewModel> Results
        {
            get { return this._results; }
            set { this.SetProperty(ref this._results, value, () => this.Results); }
        }

        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public event EventHandler<bool> CloseRequested;

        public SortingRunViewModel(List<BucketViewModel> buckets)
        {
            this.Buckets = new ObservableCollection<BucketViewModel>(buckets);

            this.CloseCommand = new DelegateCommand(this.Close, this.CanClose);
            this.CancelCommand = new DelegateCommand(this.Cancel, this.CanCancel);

            this.TotalBucketCount = buckets.Count;
            this.TotalItemCount = buckets.SelectMany(f => f.Items).Count();

            this.Results = new ObservableCollection<BucketResultViewModel>();
        }

        private bool CanClose()
        {
            return this.FinishedBucketCount == this.TotalBucketCount;
        }

        private void Close()
        {
            this.CloseRequested?.Invoke(this, true);
        }

        private bool CanCancel()
        {
            return this.CanClose() == false;
        }

        private void Cancel()
        {
            this.CloseRequested?.Invoke(this, false);
        }

        public async Task ExecuteSorting()
        {
            foreach (var bucket in this.Buckets)
            {
                this.CurrentBucket = bucket;

                this.Results.Add(await bucket.ExecuteSort());

                this.FinishedItemCount += bucket.Items.Count;
                this.FinishedBucketCount++;
            }
        }
    }
}
