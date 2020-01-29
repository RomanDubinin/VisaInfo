using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VisaProject;

namespace DataLoad
{
    public class VisaResultLoader : IVisaResultLoader
    {
        private readonly HttpClient client = new HttpClient();
        private readonly VisaInfoParser visaInfoParser = new VisaInfoParser();

        public VisaResultLoader()
        {
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        }

        public async Task<VisaResult> LoadVisaResultByStatementNumber(string statementNumber)
        {
            var content = new StringContent($"ioff_zov={statementNumber}&op=Ov%C4%9B%C5%99it&form_id=ioff_application_status_form", Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await client.PostAsync("https://frs.gov.cz/cs/ioff/application-status", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return visaInfoParser.Parse(responseString);
        }
    }
}