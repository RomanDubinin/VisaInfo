using System.IO;
using System.Linq;

namespace DataContainer
{
    public class ActualFileNameFinder : IFileNameFinder
    {
        private readonly string directory;
        public ActualFileNameFinder(string directory)
        {
            this.directory = directory;
        }

        public string FindName()
        {
            var dir = new DirectoryInfo(directory);
            return dir.GetFiles("*.txt").OrderBy(x => x.Name).Last().FullName;
        }
    }
}