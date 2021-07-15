using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_66_bit.Models;

namespace Project_66_bit.Services.ReportService
{
    public class ReportService
    {
        private ApplicationDbContext _context;
        private IProjectConverter projectConverter;

        public ReportService(ApplicationDbContext db, IProjectConverter projectConverter)
        {
            _context = db;
            this.projectConverter = projectConverter;
        }

        public async Task<byte[]> CreateReport(int projectId)
        {
            var project = await GetData(projectId);
            // MemoryStream stream = new MemoryStream();
            // Console.WriteLine(stream.Length);
            return projectConverter.FormDocument(project);
            // Console.WriteLine(stream.Length);
            // return stream;
        }

        private async Task<Project> GetData(int projectId)
        {
            var project = await _context.Projects
                .Include(proj => proj.Customer)
                .Include(proj => proj.Modules)
                .ThenInclude(x => x.Problems)
                .FirstOrDefaultAsync(proj => proj.Id == projectId);
            
            return project;
        }
    }
}