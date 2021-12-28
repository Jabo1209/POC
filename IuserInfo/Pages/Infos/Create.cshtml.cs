#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IuserInfo.Data;
using IuserInfo.Models;

namespace IuserInfo.Pages.Infos
{
    public class CreateModel : PageModel
    {
        private readonly IuserInfo.Data.IuserInfoContext _context;

        public CreateModel(IuserInfo.Data.IuserInfoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Info Info { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Info.Add(Info);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
