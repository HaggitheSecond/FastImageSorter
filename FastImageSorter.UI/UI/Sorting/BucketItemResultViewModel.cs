using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using System.IO;

namespace FastImageSorter.UI.UI.Sorting
{
    public class BucketItemResultViewModel : ViewModelBase
    {
        private FileInfo _sourceFile;
        private FileInfo _targetFile;
        private Exception _exception;
        private BucketItemResult _result;
        private TimeSpan _duration;
        private BucketItemViewModel _item;

        public FileInfo SourceFile
        {
            get { return this._sourceFile; }
            set { this.SetProperty(ref this._sourceFile, value, () => this.SourceFile); }
        }

        public FileInfo TargetFile
        {
            get { return this._targetFile; }
            set { this.SetProperty(ref this._targetFile, value, () => this.TargetFile); }
        }

        public Exception Exception
        {
            get { return this._exception; }
            set { this.SetProperty(ref this._exception, value, () => this.Exception); }
        }

        public BucketItemResult Result
        {
            get { return this._result; }
            set { this.SetProperty(ref this._result, value, () => this.Result); }
        }

        public TimeSpan Duration
        {
            get { return this._duration; }
            set { this.SetProperty(ref this._duration, value, () => this.Duration); }
        }

        public BucketItemViewModel Item
        {
            get { return this._item; }
            set { this.SetProperty(ref this._item, value, () => this.Item); }
        } 
    }
}
