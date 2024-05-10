namespace FastImageSorter.UI.Common;

public record Bucket(string Name, BucketAction Action)
{
    public List<BucketItem> Items = [];

    public BucketResult Result { get; set; }
}
