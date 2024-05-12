using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using FastImageSorter.UI.Helpers;
using FastImageSorter.UI.MVVM;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace FastImageSorter.UI.UI.Settings
{
    public class SortingSettingsViewModel : WizardStartPageViewModel<SortingRun>
    {
        private string _sourceDirectoryPath;
        private ObservableCollection<SortingSettingsBucketViewModel> _buckets;
        private SortingSettingsBucketViewModel _selectedBucket;
        private int _sourceDirectoryImageCount;
        private ObservableCollection<BucketActionType> _availableActions;

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

        public ObservableCollection<SortingSettingsBucketViewModel> Buckets
        {
            get { return this._buckets; }
            set { this.SetProperty(ref this._buckets, value, () => this.Buckets); }
        }

        public SortingSettingsBucketViewModel SelectedBucket
        {
            get { return this._selectedBucket; }
            set { this.SetProperty(ref this._selectedBucket, value, () => this.SelectedBucket); }
        }

        public ObservableCollection<BucketActionType> AvailableActions
        {
            get { return this._availableActions; }
            set { this.SetProperty(ref this._availableActions, value, () => this.AvailableActions); }
        }

        public DelegateCommand SelectSourceDirectoryCommand { get; set; }

        public DelegateCommand AddBucketCommand { get; set; }
        public DelegateCommand RemoveBucketCommand { get; set; }

        public SortingSettingsViewModel()
        {
            this.SelectSourceDirectoryCommand = new DelegateCommand(this.SelectSourceDirectory);

            this.AddBucketCommand = new DelegateCommand(this.AddBucket, () => this.Buckets.Count <= 10);
            this.RemoveBucketCommand = new DelegateCommand(this.RemoveBucket, () => this.SelectedBucket != null && this.SelectedBucket.CanBeEdited);
        }

        private void SelectSourceDirectory()
        {
            var dialog = new OpenFolderDialog
            {
                Multiselect = false
            };

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

            for (var i = 1; i < int.MaxValue; i++)
            {
                if (this.Buckets.Any(f => f.Name.EndsWith(i.ToString())) == false)
                {
                    name += i;
                    break;
                }
            }

            this.Buckets.Add(new SortingSettingsBucketViewModel()
            {
                Name = name,
                CanBeEdited = true,
            });
        }

        private async Task Accept()
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

                if (string.IsNullOrWhiteSpace(bucket.TargetDirectoryPath) && (bucket.Action == BucketActionType.Move || bucket.Action == BucketActionType.Copy))
                    messages.Add($"{safeName} does not have a target directory!");
            }

            if (messages.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, messages), "Invalid settings", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this._sortingRun = new SortingRun(this.SourceDirectoryPath, this.Buckets.Select(f => f.ToBucket()).ToList());
        }

        private SortingRun _sortingRun;

        public override SortingRun GetData()
        {
            return this._sortingRun;
        }

        public override WizardPageButton GetNext()
        {
            return new WizardPageButton("", this.Accept, () => { return true; });
        }

        public override WizardPageButton GetPrevious()
        {
            return null;
        }
    }
}
