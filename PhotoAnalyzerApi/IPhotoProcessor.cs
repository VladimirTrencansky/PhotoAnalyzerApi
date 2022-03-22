using PhotoAnalyzerApi.Objects;

namespace PhotoAnalyzerApi
{
    public interface IPhotoProcessor
    {
        IEnumerable<PhotoData> AnalyzePhotos();
    }
}