namespace Diabetes.User.FileIO
{
    public interface IFileIO
    {
        bool Exists(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string contents);
    }
}