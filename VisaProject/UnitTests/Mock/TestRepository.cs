using System.Collections.Generic;
using System.Linq;
using VisaProject;

namespace UnitTests.Mock
{
    public class TestRepository : IRepository
    {
        private List<VisaInfo> infos = new List<VisaInfo>();

        public void Write(VisaInfo info)
        {
            infos.Add(info);
        }

        public VisaInfo[] Read(VisaInfoFilter filter)
        {
            return infos.ToArray();
        }
    }
}