using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisaProject;

namespace DataLoad
{
    public class VisaResultLoader : IVisaResultLoader
    {
        private readonly int retryCount = 5;
        private readonly HttpClient client;
        private readonly VisaInfoParser visaInfoParser = new VisaInfoParser();

        public VisaResultLoader(HttpClient client)
        {
            this.client = client;
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        }

        public async Task<VisaResult> LoadVisaResultByStatementNumber(string statementNumber)
        {
            var content = new StringContent($"ioff_zov={statementNumber}&op=Ov%C4%9B%C5%99it&form_id=ioff_application_status_form", Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            for (int i = 1; i <= retryCount; i++)
            {
                try
                {
                    var response = await client.PostAsync("https://frs.gov.cz/cs/ioff/application-status", content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    return visaInfoParser.Parse(responseString);
                }
                catch (Exception e)
                {
                    if (i == retryCount)
                    {
                        throw;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }
            throw new Exception("Must never be thrown");
        }
    }
}