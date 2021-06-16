using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_66_bit.Models;

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

        public IndexModel(ApplicationDbContext db)
        {
            _context = db;
            NewProject = new Project();
            NewCustomer = new Customer();
        }

        public void OnGetAsync()
        {
            Projects = _context.Projects.ToList();
            Modules = _context.Modules.ToList();
            Customers = _context.Customers.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                
                _context.Customers.Add(NewCustomer);
                await _context.SaveChangesAsync();
                
                NewProject.Customer = _context.Customers
                    .OrderByDescending(t => t.Id)
                    .FirstOrDefault();

                _context.Projects.Add(NewProject);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }

        public string GetProjectStatus(ProjectStatus status_enum)
        {
            switch (status_enum)
            {
                case ProjectStatus.Planning:
                    return "Планирование";
                case ProjectStatus.Complete:
                    return "Завершен";
                case ProjectStatus.InDevelopment:
                    return "В разработке";
                default:
                    return "Пред. продажа";
            }
        }
    }
}
