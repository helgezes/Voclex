namespace RazorLibrary.DataTransferObjects
{
    public sealed class ImageSuggestion
    {
        public ImageSuggestion(string fullSizeUrl, string previewUrl)
        {
            FullSizeUrl = fullSizeUrl;
            PreviewUrl = previewUrl;
        }

        public string PreviewUrl { get; }
        public string FullSizeUrl { get; }
    }
}
