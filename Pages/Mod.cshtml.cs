using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_66_bit.Models;

namespace RazorProject.Pages
{
    public class ModModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Project Proj { get; set; }
        public Customer Customer { get; set; }
        public List<Module> Modules { get; set; }
        public List<Project_66_bit.Models.Task> Tasks { get; set; }
        public ModModel(ApplicationDbContext db)
        {
            _context = db;
        }
        public void OnGetAsync(int id)
        {
            Proj = _context.Projects.FindAsync(id).Result;
            Customer = Proj.Customer;
            Modules = _context.Modules.Where(m => m.ProjectId == id).ToList();
            Tasks = _context.Tasks.ToList();
        }
    }
}
