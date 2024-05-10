using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using System.IO;
using System.Windows.Media.Imaging;

namespace FastImageSorter.UI.UI.Sorting
{
    public class BucketItemViewModel : ViewModelBase
    {
        private FileInfo _file;
        private string _path;
        private string _name;
        private BucketViewModel? _bucket;

        private BitmapImage? _image;

        public FileInfo File
        {
            get { return this._file; }
            set { this.SetProperty(ref this._file, value, () => this.File); }
        }

        public string Path
        {
            get { return this._path; }
            set { this.SetProperty(ref this._path, value, () => this.Path); }
        }


        public string Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value, () => this.Name); }
        }

        public BitmapImage? Image
        {
            get { return this._image; }
            set { this.SetProperty(ref this._image, value, () => this.Image); }
        }

        public BucketViewModel? Bucket
        {
            get { return this._bucket; }
            set { this.SetProperty(ref this._bucket, value, () => this.Bucket); }
        }

        public BucketItemViewModel(FileInfo fileInfo)
        {
            this.File = fileInfo;
            this.Path = fileInfo.FullName;
            this.Name = fileInfo.Name;
        }

        public void Activate()
        {
            this.Image = new BitmapImage(new Uri(this.Path));
        }

        public void Deactivate()
        {
            this.Image = null;
        }

        public async Task<BucketItemResultViewModel> ExecuteSort(BucketViewModel bucket)
        {
            var result = new BucketItemResultViewModel
            {
                SourceFile = this.File
            };

            try
            {
                if (this.Bucket == null)
                {
                    result.Result = BucketItemResult.Failure;
                    result.Exception = new Exception("No bucket set!");
                }
                else
                {
                    var targetFileName = System.IO.Path.Combine(bucket.TargetDirectoryPath, this.File.Name);

                    switch (this.Bucket.Action)
                    {
                        case BucketAction.Skip:
                            break;
                        case BucketAction.Move:
                            System.IO.File.Move(this.Path, targetFileName);
                            break;
                        case BucketAction.Copy:
                            System.IO.File.Copy(this.Path, targetFileName);
                            break;
                        case BucketAction.Delete:
                            System.IO.File.Delete(this.Path);
                            break;
                    }
                }                
            }
            catch (Exception e)
            {
                result.Exception = e;
                result.Result = BucketItemResult.Failure;
            }

            return result;
        }
    }
}
