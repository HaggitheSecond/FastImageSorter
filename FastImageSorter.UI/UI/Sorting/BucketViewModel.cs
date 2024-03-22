using DevExpress.Mvvm;
using FastImageSorter.UI.UI.KeySelection;
using FastImageSorter.UI.UI.Sorting;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastImageSorter.UI.UI
{
    public class BucketViewModel : ViewModelBase
    {
		private string _name;
        private string _targetDirectoryPath;
        private Key? _key;
        private ObservableCollection<BucketItemViewModel> items;

        public string Name
		{
			get { return this._name; }
			set { this.SetProperty(ref this._name, value, () => this.Name); }
		}

        public string TargetDirectoryPath
		{
			get { return this._targetDirectoryPath; }
			set { this.SetProperty(ref this._targetDirectoryPath, value, () => this.TargetDirectoryPath); }
		}

		public Key? Key
		{
			get { return this._key; }
			set { this.SetProperty(ref this._key, value, () => this.Key); }
		}

        public ObservableCollection<BucketItemViewModel> Items
        {
            get { return this.items; }
            set { this.SetProperty(ref this.items, value, () => this.Items); }
        }

        public DelegateCommand SetTargetDirectoryCommand { get; }

		public DelegateCommand SetKeyCommand { get; }

        public BucketViewModel()
        {
            this.SetTargetDirectoryCommand = new DelegateCommand(this.SetTargetDirectory);
            this.SetKeyCommand = new DelegateCommand(this.SetKey);

            this.Items = new ObservableCollection<BucketItemViewModel>();
        }

        private void SetKey()
        {
            var view = new KeySelectionView();

            if (view.ShowDialog().GetValueOrDefault() && view.Key != null)
            {
                this.Key = view.Key.Value;
            }
        }

        private void SetTargetDirectory()
        {
            var dialog = new OpenFolderDialog();

            dialog.Multiselect = false;

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                this.TargetDirectoryPath = dialog.FolderName;
            }
        }
    }
}
