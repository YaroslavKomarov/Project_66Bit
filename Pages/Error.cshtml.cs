using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorProject.Pages
{
    public class ErrorModel : PageModel
    {
        public int Code { get; set; }
        public string RequestId { get; set; }

        public IActionResult OnGet(int? code)
        {
            Code = code ?? 0;
            if (Code == 0)
            {
                return BadRequest();
            }
            
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return Page();
        }
    }
}
