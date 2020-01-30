using DataContainer;

namespace UnitTests.Mock
{
    public class TestFileNameFinder : IFileNameFinder
    {
        private string fileName;

        public TestFileNameFinder(string fileName)
        {
            this.fileName = fileName;
        }

        public string FindName()
        {
            return fileName;
        }
    }
}