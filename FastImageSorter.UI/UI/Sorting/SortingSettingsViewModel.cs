using DevExpress.Mvvm;
using FastImageSorter.UI.Helpers;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace FastImageSorter.UI.UI.Sorting
{
    public class SortingSettingsViewModel : ViewModelBase
    {
        private string _sourceDirectoryPath;
        private ObservableCollection<BucketViewModel> _buckets;
        private BucketViewModel _selectedBucket;
        private int _sourceDirectoryImageCount;

        public string SourceDirectoryPath
        {
            get { return this._sourceDirectoryPath; }
            set
            {
                this.SetProperty(ref this._sourceDirectoryPath, value, () => this.SourceDirectoryPath);


                if (string.IsNullOrWhiteSpace(value))
                    this.SourceDirectoryImageCount = 0;
                else
                    this.SourceDirectoryImageCount = DirectoryHelper.GetImageFiles(value).Count();
            }
        }

        public int SourceDirectoryImageCount
        {
            get { return this._sourceDirectoryImageCount; }
            set { this.SetProperty(ref this._sourceDirectoryImageCount, value, () => this.SourceDirectoryImageCount); }
        }

        public ObservableCollection<BucketViewModel> Buckets
        {
            get { return this._buckets; }
            set { this.SetProperty(ref this._buckets, value, () => this.Buckets); }
        }

        public BucketViewModel SelectedBucket
        {
            get { return this._selectedBucket; }
            set { this.SetProperty(ref this._selectedBucket, value, () => this.SelectedBucket); }
        }

        public DelegateCommand SelectSourceDirectoryCommand { get; set; }

        public DelegateCommand AddBucketCommand { get; set; }
        public DelegateCommand RemoveBucketCommand { get; set; }

        public DelegateCommand AcceptCommand { get; set; }

        public event EventHandler SettingsAcceptedEvent;

        public SortingSettingsViewModel()
        {
            this.SelectSourceDirectoryCommand = new DelegateCommand(this.SelectSourceDirectory);

            this.AddBucketCommand = new DelegateCommand(this.AddBucket, () => this.Buckets.Count <= 10);
            this.RemoveBucketCommand = new DelegateCommand(this.RemoveBucket, () => this.SelectedBucket != null);

            this.AcceptCommand = new DelegateCommand(this.Accept);

            this.Buckets = new ObservableCollection<BucketViewModel>();

            if (Debugger.IsAttached)
            {
                this.SourceDirectoryPath = @"C:\Users\Haggi\Desktop\temp";

                this.Buckets.Add(new BucketViewModel
                {
                    Key = System.Windows.Input.Key.A,
                    Name = "Bucket A",
                    TargetDirectoryPath = Path.Combine(this.SourceDirectoryPath, "A")
                });

                this.Buckets.Add(new BucketViewModel
                {
                    Key = System.Windows.Input.Key.B,
                    Name = "Bucket B",
                    TargetDirectoryPath = Path.Combine(this.SourceDirectoryPath, "B")
                });
            }
        }

        private void SelectSourceDirectory()
        {
            var dialog = new OpenFolderDialog();

            dialog.Multiselect = false;

            if (dialog.ShowDialog().GetValueOrDefault() == false || Directory.Exists(dialog.FolderName) == false)
                return;

            this.SourceDirectoryPath = dialog.FolderName;
        }

        private void RemoveBucket()
        {
            var index = this.Buckets.IndexOf(this.SelectedBucket);

            this.Buckets.Remove(this.SelectedBucket);

            if (this.Buckets.Count > 0)
                this.SelectedBucket = this.Buckets.ElementAt(index == this.Buckets.Count ? --index : index);
        }

        private void AddBucket()
        {
            var name = "Bucket ";

            for (int i = 1; i < int.MaxValue; i++)
            {
                if (this.Buckets.Any(f => f.Name.EndsWith(i.ToString())) == false)
                {
                    name += i;
                    break;
                }
            }

            this.Buckets.Add(new BucketViewModel()
            {
                Name = name
            });
        }

        private void Accept()
        {
            var messages = new List<string>();

            if (string.IsNullOrWhiteSpace(this.SourceDirectoryPath))
                messages.Add("No source directory set!");

            if (this.Buckets.Count == 0)
                messages.Add("No buckets set!");

            if (this.Buckets.Count == 1)
                messages.Add("Atleast 2 buckets must be set!");

            foreach (var bucket in this.Buckets)
            {
                var safeName = string.IsNullOrWhiteSpace(bucket.Name) ? "Bucket #" + (this.Buckets.IndexOf(bucket) + 1) : bucket.Name;

                if (string.IsNullOrEmpty(bucket.Name))
                    messages.Add($"{safeName} does not have a name!");

                if (bucket.Key == null)
                    messages.Add($"{safeName} does not have a key!");

                if (string.IsNullOrWhiteSpace(bucket.TargetDirectoryPath))
                    messages.Add($"{safeName} does not have a target directory!");
            }

            if (messages.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, messages), "Invalid settings", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.SettingsAcceptedEvent?.Invoke(this, new EventArgs());
        }
    }
}
