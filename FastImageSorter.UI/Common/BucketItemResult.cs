namespace FastImageSorter.UI.Common
{
    public record BucketItemResult(BucketItemResultType ResultType)
    {
        public Exception Exception { get; set; }
    }
}
