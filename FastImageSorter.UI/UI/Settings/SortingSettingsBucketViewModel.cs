using DevExpress.Mvvm;
using FastImageSorter.UI.Common;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FastImageSorter.UI.UI.Settings;

public class SortingSettingsBucketViewModel : ViewModelBase
{
    private string _name;
    private string _targetDirectoryPath;
    private Key? _key;

    private BucketActionType _action;
    private ObservableCollection<BucketActionType> _availableActions;

    private bool _canBeEdited;

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

    public BucketActionType Action
    {
        get { return this._action; }
        set { this.SetProperty(ref this._action, value, () => this.Action); }
    }

    public ObservableCollection<BucketActionType> AvailableActions
    {
        get { return this._availableActions; }
        set { this.SetProperty(ref this._availableActions, value, () => this.AvailableActions); }
    }

    public bool CanBeEdited
    {
        get { return this._canBeEdited; }
        set { this.SetProperty(ref this._canBeEdited, value, () => this.CanBeEdited); }
    }

    public DelegateCommand SetTargetDirectoryCommand { get; }
    public DelegateCommand SetKeyCommand { get; }

    public SortingSettingsBucketViewModel()
    {
        this.SetTargetDirectoryCommand = new DelegateCommand(this.SetTargetDirectory, () => this.CanBeEdited);
        this.SetKeyCommand = new DelegateCommand(this.SetKey, () => this.CanBeEdited);

        this.AvailableActions = new ObservableCollection<BucketActionType>(Enum.GetValues<BucketActionType>());
    }

    private void SetKey()
    {
        var view = new KeySelectionView();

        if (view.ShowDialog().GetValueOrDefault() && view.Key != null)
            this.Key = view.Key.Value;
    }

    private void SetTargetDirectory()
    {
        var dialog = new OpenFolderDialog
        {
            Multiselect = false
        };

        if (dialog.ShowDialog().GetValueOrDefault())
            this.TargetDirectoryPath = dialog.FolderName;
    }

    public Bucket ToBucket()
    {
        return new Bucket(this.Name, new BucketAction(this.Action, this.Key.GetValueOrDefault())
        {
            TargetDirectoryPath = this.TargetDirectoryPath,
        });
    }
}
