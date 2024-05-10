using System.Windows.Input;

namespace FastImageSorter.UI.Common;

public record BucketAction(BucketActionType Type, Key Key)
{
    public string TargetDirectoryPath { get; set; }
}
