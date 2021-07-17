using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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
        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
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

        public async Task<IActionResult> OnPostDeleteProblemAsync(int id, int modId, int projId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var delProblem = await _context.Problems.FindAsync(id);
            var moduleParent = await _context.Modules.FindAsync(modId);
            moduleParent.Hours -= delProblem.Hours;

            _context.Problems.Remove(delProblem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Mod", new { id = projId });
        }


        public async Task<ContentResult> OnGetModulesAsync()
        {
            var allModules = await _context.Modules
                .Select(m => new { Id = m.Id, Name = m.Name, ProjectName = m.Project.Name })
                .ToListAsync();

            return Content(JsonSerializer.Serialize(allModules));
        }

        public async Task<IActionResult> OnPostDeleteModAsync(int id, int projId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var delModule = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(delModule);
            await _context.SaveChangesAsync();

            return RedirectToPage("Mod", new { id = projId });
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
            if (!ModelState.IsValid)
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
            if (!ModelState.IsValid)
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
        
        public async Task<IActionResult> OnPostEditProjectAsync(int projId, int custId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Project.Id = projId;
            Customer.Id = custId;
            Project.CustomerId = Customer.Id;
            _context.Update(Project);
            _context.Update(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("Mod", new { id = projId });
        }

        public async Task<JsonResult> OnPostCopyModuleAsync(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            var copyModule = new Module
            {
                Name = module.Name,
                Hours = module.Hours,
                Project = await _context.Projects.FindAsync(Project.Id)
            };
            await _context.Modules.AddAsync(copyModule);

            foreach (var problem in _context.Problems.Where(p => p.ModuleId == id))
            {
                var copyProblem = new Problem
                {
                    Name = problem.Name,
                    Hours = problem.Hours,
                    StartDate = problem.StartDate,
                    EndDate = problem.EndDate,
                    Module = copyModule
                };
                await _context.Problems.AddAsync(copyProblem);
            }
            await _context.SaveChangesAsync();

            return new JsonResult(new { Url = $"Mod?id={Project.Id}", status = "OK" });
        }
    }
}
