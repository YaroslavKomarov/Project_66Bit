using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_66_bit.Models;

namespace RazorProject.Pages
{
    public class ModModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Project> Task { get; set; }
        public ModModel(ApplicationDbContext db)
        {
            _context = db;
        }
        public void OnGet()
        {
            Task = _context.Projects.ToList();
        }
    }
}
