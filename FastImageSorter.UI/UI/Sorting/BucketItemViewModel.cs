using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FastImageSorter.UI.UI.Sorting
{
    public class BucketItemViewModel : ViewModelBase
    {
        private FileInfo _file;
        private string _path;
        private string _name;

        private BitmapImage _image;

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

        public BitmapImage Image
        {
            get { return this._image; }
            set { this.SetProperty(ref this._image, value, () => this.Image); }
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
    }
}
