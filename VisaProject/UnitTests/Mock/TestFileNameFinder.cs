using System.IO;
using DataContainer;

namespace UnitTests.Mock
{
    public class TestFileNameFinder : IFileNameFinder
    {
        private string directory;
        private string filename;

        public TestFileNameFinder(string directory, string filename)
        {
            this.directory = directory;
            this.filename = filename;
        }

        public string FindName(string subDirectory)
        {
            return Path.Combine(directory, subDirectory, filename);
        }
    }
}