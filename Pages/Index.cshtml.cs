using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_66_bit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            await _context.Customers.AddAsync(NewCustomer);
            await _context.SaveChangesAsync();

            NewProject.Customer = await _context.Customers
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            await _context.Projects.AddAsync(NewProject);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
