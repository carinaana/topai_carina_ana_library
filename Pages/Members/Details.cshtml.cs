using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using topai_carina_ana_lab2.Data;
using topai_carina_ana_lab2.Models;

namespace topai_carina_ana_lab2.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly topai_carina_ana_lab2.Data.topai_carina_ana_lab2Context _context;

        public DetailsModel(topai_carina_ana_lab2.Data.topai_carina_ana_lab2Context context)
        {
            _context = context;
        }

        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }
            else
            {
                Member = member;
            }
            return Page();
        }
    }
}
