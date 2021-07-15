using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_66_bit.Models;
using Project_66_bit.Services.ReportService;

namespace RazorProject.Pages
{
    public class ModModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private ReportService reportService;
        public int IsOpenProblems { get; set; }
        public Project Project { get; set; }
        public Customer Customer { get; set; }
        public List<Module> Modules { get; set; }
        public List<Problem> Problems { get; set; }
        [BindProperty]
        public Module NewModule { get; set; }
        [BindProperty]
        public Problem NewProblem { get; set; }
        public ModModel(ApplicationDbContext db, ReportService reportService)
        {
            _context = db;
            this.reportService = reportService;
        }

        public async Task OnGetAsync(int id, int? modId)
        {
            IsOpenProblems = modId != null ? (int)modId : -1;
            Project = await _context.Projects.FindAsync(id);
            Customer = await _context.Customers.FindAsync(Project.CustomerId);
            Modules = await _context.Modules.Where(m => m.ProjectId == id).ToListAsync();
            Problems = await _context.Problems.ToListAsync();

            Modules.Reverse();
        }

        public async Task<IActionResult> OnPostDeleteProblemAsync(int id, int idProj)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var delProblem = await _context.Problems.FindAsync(id);
            _context.Problems.Remove(delProblem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Mod", new { id = idProj });
        }

        public async Task<IActionResult> OnPostDeleteModAsync(int id, int idProj)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var delModule = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(delModule);
            await _context.SaveChangesAsync();

            return RedirectToPage("Mod", new { id = idProj });
        }

        public async Task<IActionResult> OnPostDeleteProjAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var delProject = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(delProject);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostModuleAsync(int id)
        {
            if (NewModule == null)
            {
                return Page();
            }

            NewModule.Project = await _context.Projects.FindAsync(id);
            await _context.Modules.AddAsync(NewModule);
            await _context.SaveChangesAsync();

            return RedirectToPage("Mod", new { id = id });
        }

        public async Task<IActionResult> OnPostProblemAsync(int id, int projId)
        {
            if (NewProblem == null)
            {
                return Page();
            }

            NewProblem.Module = await _context.Modules.FindAsync(id);
            NewProblem.Module.Hours += NewProblem.Hours;
            await _context.Problems.AddAsync(NewProblem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Mod", new { id = projId });
        }

        public async Task<IActionResult> OnPostDownloadExcelAsync(int projectId)
        {
            // var buffer = new MemoryStream();
            var fileBytes = await reportService.CreateReport(projectId);
            // await fileBytes.CopyToAsync(buffer);

            var fileStreamResult = new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            fileStreamResult.FileDownloadName = $"Project-{projectId}.xlsx";
            return fileStreamResult;
        }
    }
}
