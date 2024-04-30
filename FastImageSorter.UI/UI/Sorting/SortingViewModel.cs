﻿using DevExpress.Mvvm;
using FastImageSorter.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastImageSorter.UI.UI.Sorting;
public class SortingViewModel : ViewModelBase
{
    private int _totalImageCount;
    private int _finishedImageCount;
    private bool _finishedSorting;
    private SortingSettingsViewModel _settingsViewModel;
    private ObservableCollection<BucketItemViewModel> _unsortedItems;
    private BucketItemViewModel _selectedUnsortedItem;

    public int TotalImageCount
    {
        get { return this._totalImageCount; }
        set { this.SetProperty(ref this._totalImageCount, value, () => this.TotalImageCount); }
    }

    public int FinishedImageCount
    {
        get { return this._finishedImageCount; }
        set { this.SetProperty(ref this._finishedImageCount, value, () => this.FinishedImageCount); }
    }

    public bool FinishedSorting
    {
        get { return this._finishedSorting; }
        set { this.SetProperty(ref this._finishedSorting, value, () => this.FinishedSorting); }
    }

    public SortingSettingsViewModel SettingsViewModel
    {
        get { return this._settingsViewModel; }
        set { this.SetProperty(ref this._settingsViewModel, value, () => this.SettingsViewModel); }
    }

    public ObservableCollection<BucketItemViewModel> UnsortedItems
    {
        get { return this._unsortedItems; }
        set { this.SetProperty(ref this._unsortedItems, value, () => this.UnsortedItems); }
    }

    public BucketItemViewModel SelectedUnsortedItem
    {
        get { return this._selectedUnsortedItem; }
        set
        {

            if (value != null && value.Image == null)
                value.Activate();

            this.SetProperty(ref this._selectedUnsortedItem, value, () => this.SelectedUnsortedItem);
        }
    }

    public DelegateCommand ExecuteSortingCommand { get; }
    public DelegateCommand CancelSortingCommand { get; }

    public event EventHandler FinishedSortingEvent;

    public SortingViewModel(SortingSettingsViewModel sortingSettingsViewModel)
    {
        this.ExecuteSortingCommand = new DelegateCommand(this.ExecuteSorting, this.CanExecuteSorting);
        this.CancelSortingCommand = new DelegateCommand(() => this.FinishedSortingEvent?.Invoke(this, EventArgs.Empty));

        this.SettingsViewModel = sortingSettingsViewModel;

        var files = DirectoryHelper.GetImageFiles(this.SettingsViewModel.SourceDirectoryPath);

        this.TotalImageCount = files.Count();
        this.FinishedImageCount = 0;

        this.UnsortedItems = new ObservableCollection<BucketItemViewModel>(files.Select(f => new BucketItemViewModel(new FileInfo(f))));
        this.SelectedUnsortedItem = this.UnsortedItems.First();
    }

    private bool CanExecuteSorting()
    {
        return true;
    }

    private void ExecuteSorting()
    {
        foreach (var bucket in this.SettingsViewModel.Buckets)
        {
            if (bucket.Action == BucketAction.Skip)
                continue;

            foreach (var item in bucket.Items)
            {
                if (bucket.Action == BucketAction.Delete)
                {
                    File.Delete(item.Path);
                    continue;
                }

                var targetFileName = Path.Combine(bucket.TargetDirectoryPath, item.File.Name);

                if (File.Exists(targetFileName))
                    continue;

                if (bucket.Action == BucketAction.Move)
                {
                    File.Move(item.Path, targetFileName);
                }
                else
                {
                    File.Copy(item.Path, targetFileName);
                }
            }
        }
    }

    public void SortItem(Key key)
    {
        if (this.UnsortedItems.Count == 0)
            return;

        var bucket = this.SettingsViewModel.Buckets.FirstOrDefault(b => b.Key == key);

        if (bucket != null)
        {
            bucket.Items.Add(this.SelectedUnsortedItem);

            this.UnsortedItems.RemoveAt(0);

            if (this.UnsortedItems.Count == 0)
                this.SelectedUnsortedItem = null;
            else
                this.SelectedUnsortedItem = this.UnsortedItems.First();

            this.FinishedImageCount++;

            this.FinishedSorting = this.UnsortedItems.Count == 0;
        }
    }
}
