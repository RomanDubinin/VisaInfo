using System.IO;
using System.Linq;

namespace VisaProject
{
    public class VisaRepository : IRepository
    {
        private string fileName;

        public VisaRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public void Write(VisaInfo info)
        {
            File.AppendAllText(fileName, $"{info.ToString()}\n");
        }

        public void RewriteAll(VisaInfo[] infos)
        {
            var data = string.Join("\n", infos.Select(x => x.ToString()));
            File.WriteAllText(fileName, data);
        }

        public VisaInfo[] ReadAll()
        {
            var visaStrings = File.ReadLines(fileName);

            return visaStrings
                   .Select(x => new VisaInfo(x))
                   .ToArray();
        }
    }
}