using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using topai_carina_ana_lab2.Data;
using topai_carina_ana_lab2.Models;

namespace topai_carina_ana_lab2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly topai_carina_ana_lab2.Data.topai_carina_ana_lab2Context _context;

        public DetailsModel(topai_carina_ana_lab2.Data.topai_carina_ana_lab2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
               .Include(b => b.Member)
               .Include(b => b.Book).ThenInclude(book => book.Author)
               .Include(b => b.Book).ThenInclude(book => book.Publisher)
               .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing == null)
            {
                return NotFound();
            }
            else
            {
                Borrowing = borrowing;
            }



            return Page();
        }
    }
}
