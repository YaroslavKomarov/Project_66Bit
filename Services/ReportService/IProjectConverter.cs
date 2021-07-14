using System.IO;
using Project_66_bit.Models;

namespace Project_66_bit.Services.ReportService
{
    public interface IProjectConverter
    {
        void FormDocument(FileStream file, Project project);
    }
}