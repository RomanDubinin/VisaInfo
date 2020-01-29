using System.Threading.Tasks;
using DataLoad;
using VisaProject;

namespace UnitTests.Mock
{
    public class TestVisaResultLoader : IVisaResultLoader
    {
        public async Task<VisaResult> LoadVisaResultByStatementNumber(string statementNumber)
        {
            return VisaResult.InService;
        }
    }
}