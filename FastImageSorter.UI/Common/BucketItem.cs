using System.IO;

namespace FastImageSorter.UI.Common;

public record BucketItem(FileInfo File)
{
    public BucketItemResult Result { get; set; }
}
