using System.IO;


namespace Diabetes.User.FileIO
{
    public class FileIO : IFileIO
    {
        public bool Exists(string path)
        {
            return File.Exists(path: path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path: path);
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path: path, contents: contents);
        }
    }
}