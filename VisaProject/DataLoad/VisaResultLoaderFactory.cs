using System.Net.Http;
using DotNetTor.SocksPort;

namespace DataLoad
{
    public static class VisaResultLoaderFactory
    {
        public static VisaResultLoader GetRegularLoader()
        {
            return new VisaResultLoader(new HttpClient());
        }

        public static VisaResultLoader GetTorLoader()
        {
            return new VisaResultLoader(new HttpClient(new SocksPortHandler("127.0.0.1", 9050)));
        }
    }
}