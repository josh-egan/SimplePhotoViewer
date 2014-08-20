namespace SimplePhotoViewer.IO
{
    public interface ICanSelectFile
    {
        string SelectFile(params string[] extensions);
    }
}