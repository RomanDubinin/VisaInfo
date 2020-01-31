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
            var fileName = actualFileNameFinder.FindName(info.City);
            File.AppendAllText(fileName, $"{info.ToString()}\n");
        }

        public VisaInfo[] Read(VisaInfoFilter filter)
        {
            var fileName = actualFileNameFinder.FindName(filter.City);
            var visaStrings = File.ReadLines(fileName);

            return visaStrings
                   .Select(x => new VisaInfo(x))
                   .ToArray();
        }
    }
}