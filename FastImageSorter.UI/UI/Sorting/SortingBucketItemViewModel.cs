using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using System.IO;
using System.Windows.Media.Imaging;

namespace FastImageSorter.UI.UI.Sorting
{
    public class SortingBucketItemViewModel : ViewModelBase
    {
        private FileInfo _file;
        private SortingBucketViewModel? _bucket;

        private BitmapImage? _image;

        public FileInfo File
        {
            get { return this._file; }
            set { this.SetProperty(ref this._file, value, () => this.File); }
        }
        
        public BitmapImage? Image
        {
            get { return this._image; }
            set { this.SetProperty(ref this._image, value, () => this.Image); }
        }

        public SortingBucketViewModel? Bucket
        {
            get { return this._bucket; }
            set { this.SetProperty(ref this._bucket, value, () => this.Bucket); }
        }

        public SortingBucketItemViewModel(FileInfo fileInfo)
        {
            this.File = fileInfo;
        }

        public void Activate()
        {
            this.Image = new BitmapImage(new Uri(this.File.FullName));
        }

        public void Deactivate()
        {
            this.Image = null;
        }

        public BucketItem ToItem()
        {
            return new BucketItem(this.File);
        }
    }
}
