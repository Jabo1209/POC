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
    public class IndexModel : PageModel
    {
        private readonly IuserInfo.Data.IuserInfoContext _context;

        public IndexModel(IuserInfo.Data.IuserInfoContext context)
        {
            _context = context;
        }

        public IList<Info> Info { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var accounts = from a in _context.Info
                         select a;
            if (!string.IsNullOrEmpty(SearchString))
            {
                accounts = accounts.Where(s => s.IuserAccount.Contains(SearchString));
            }

            Info = await accounts.ToListAsync();

            //Info = await _context.Info.ToListAsync();
        }
    }
}
