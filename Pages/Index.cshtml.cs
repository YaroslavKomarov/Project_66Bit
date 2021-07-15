using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_66_bit.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace RazorProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Project> Projects { get; set; }
        public List<Module> Modules { get; set; }
        public List<Customer> Customers { get; set; }
        [BindProperty]
        public Project NewProject { get; set; }
        [BindProperty]
        public Customer NewCustomer { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            NewProject = new Project();
            NewCustomer = new Customer();
        }

        public async Task OnGetAsync()
        {
            Projects = await _context.Projects.ToListAsync();
            Modules = await _context.Modules.ToListAsync();
            Customers = await _context.Customers.ToListAsync();

            Projects.Reverse();
            Modules.Reverse();
            Customers.Reverse();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var tmpCustomer = await _context.Customers
                .Where(c => c.Name == NewCustomer.Name && c.Email == NewCustomer.Email && c.PhoneNumber == NewCustomer.PhoneNumber)
                .FirstOrDefaultAsync();

            if (tmpCustomer != null)
            {
                NewProject.Customer = tmpCustomer;
            }
            else
            {
                await _context.Customers.AddAsync(NewCustomer);
                await _context.SaveChangesAsync();

                NewProject.Customer = await _context.Customers
                    .OrderByDescending(t => t.Id)
                    .FirstOrDefaultAsync();
            }


            await _context.Projects.AddAsync(NewProject);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, bool isProj)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (isProj)
            {
                var delProject = await _context.Projects.FindAsync(id);
                _context.Projects.Remove(delProject);
            }
            else
            {
                var delModule = await _context.Modules.FindAsync(id);
                _context.Modules.Remove(delModule);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostCopyprojectAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var tmpProj = await _context.Projects.FindAsync(id);

            Project copyProj = new Project()
            {
                Name = $"{tmpProj.Name} (copy)",
                Status = tmpProj.Status,
                Cost = tmpProj.Cost,
                Type = tmpProj.Type,
                StartDate = tmpProj.StartDate,
                EndDate = tmpProj.EndDate,
                Customer = await _context.Customers.FindAsync(tmpProj.CustomerId)
            };

            await _context.Projects.AddAsync(copyProj);

            var lstOfTuples = new List<Tuple<int, Module>>();

            foreach (var module in _context.Modules.Where(m => m.ProjectId == id))
            {
                var copyModule = new Module()
                {
                    Name = module.Name,
                    Hours = module.Hours,
                    Project = copyProj
                };
                await _context.Modules.AddAsync(copyModule);

                lstOfTuples.Add(new Tuple<int, Module>(module.Id, copyModule));
            }

            foreach (var tuple in lstOfTuples)
            {
                foreach (var problem in _context.Problems.Where(p => p.ModuleId == tuple.Item1))
                {
                    var copyProblem = new Problem()
                    {
                        Name = problem.Name,
                        Hours = problem.Hours,
                        StartDate = problem.StartDate,
                        EndDate = problem.EndDate,
                        Module = tuple.Item2
                    };
                    await _context.Problems.AddAsync(copyProblem);
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
