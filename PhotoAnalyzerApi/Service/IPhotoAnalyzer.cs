using PhotoAnalyzerApi.Objects;

namespace PhotoAnalyzerApi.Service
{
    public interface IPhotoAnalyzer
    {
        public IEnumerable<PhotoData> SaveAndAnalyzePhotos(FileModel files);
    }
}
