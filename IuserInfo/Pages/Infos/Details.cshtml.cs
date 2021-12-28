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
    public class DetailsModel : PageModel
    {
        private readonly IuserInfo.Data.IuserInfoContext _context;

        public DetailsModel(IuserInfo.Data.IuserInfoContext context)
        {
            _context = context;
        }

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
    }
}
