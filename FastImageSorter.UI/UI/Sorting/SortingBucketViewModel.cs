using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
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
    public class SortingBucketViewModel : ViewModelBase
    {
        private ObservableCollection<SortingBucketItemViewModel> _items;

        public ObservableCollection<SortingBucketItemViewModel> Items
        {
            get { return this._items; }
            set { this.SetProperty(ref this._items, value, () => this.Items); }
        }

        public SortingBucketViewModel()
        {
            this.Items = [];
        }
    }
}
