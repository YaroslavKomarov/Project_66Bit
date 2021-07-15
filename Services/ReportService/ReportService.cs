using System.IO;
using System.Threading.Tasks;
using Project_66_bit.Models;

namespace Project_66_bit.Services.ReportService
{
    public class ReportService
    {
        private IProjectConverter projectConverter;

        public async Task<FileStream> CreateReport(string projectId)
        {
            throw new System.NotImplementedException();
        }

        private async Task<Project> GetData(string projectId)
        {
            throw new System.NotImplementedException();
        }
    }
}