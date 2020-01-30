using System.IO;
using System.Linq;
using VisaProject;

namespace DataContainer
{
    public class VisaRepository : IRepository
    {
        private IFileNameFinder actualFileNameFinder;

        public VisaRepository(IFileNameFinder actualFileNameFinder)
        {
            this.actualFileNameFinder = actualFileNameFinder;
        }

        public void Write(VisaInfo info)
        {
            File.AppendAllText(actualFileNameFinder.FindName(), $"{info.ToString()}\n");
        }

        public void RewriteAll(VisaInfo[] infos)
        {
            var data = string.Join("\n", infos.Select(x => x.ToString()));
            File.WriteAllText(actualFileNameFinder.FindName(), data);
        }

        public VisaInfo[] ReadAll()
        {
            var visaStrings = File.ReadLines(actualFileNameFinder.FindName());

            return visaStrings
                   .Select(x => new VisaInfo(x))
                   .ToArray();
        }
    }
}