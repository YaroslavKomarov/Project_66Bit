using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_66_bit.Models;
using ASPNET_MVC.Models;

namespace RazorProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public List<Project> Projecte { get; set; }
        public Project Person { get; set; }

        public IndexModel(ApplicationDbContext logger)
        {
            db = logger;
        }

        public void OnGet()
        {
            Projecte = db.Projects.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(Person);
                await db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
