using System.Threading.Tasks;
using VisaProject;

namespace DataLoad
{
    public interface IVisaResultLoader
    {
        Task<VisaResult> LoadVisaResultByStatementNumber(string statementNumber);
    }
}