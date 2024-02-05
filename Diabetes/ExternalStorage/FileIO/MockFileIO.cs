namespace Diabetes.User.FileIO
{
    public class MockFileIO : IFileIO
    {
        public bool FileExistsResult { get; set; }
        public string ReadAllTextResult { get; set; }
        
        public bool Exists(string path)
        {
            return FileExistsResult;
        }

        public string ReadAllText(string path)
        {
            return ReadAllTextResult;
        }

        public void WriteAllText(string path, string contents)
        {
            // Implement method if needed in test
        }
    }
}