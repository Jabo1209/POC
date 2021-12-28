#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IuserInfo.Data;
using IuserInfo.Models;

namespace IuserInfo.Pages.Infos
{
    public class DeleteModel : PageModel
    {
        private readonly IuserInfo.Data.IuserInfoContext _context;

        public DeleteModel(IuserInfo.Data.IuserInfoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Info Info { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Info = await _context.Info.FirstOrDefaultAsync(m => m.ID == id);

            if (Info == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Info = await _context.Info.FindAsync(id);

            if (Info != null)
            {
                _context.Info.Remove(Info);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
