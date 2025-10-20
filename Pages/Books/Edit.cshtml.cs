using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using topai_carina_ana_lab2.Data;
using topai_carina_ana_lab2.Models;

namespace topai_carina_ana_lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly topai_carina_ana_lab2.Data.topai_carina_ana_lab2Context _context;

        public EditModel(topai_carina_ana_lab2.Data.topai_carina_ana_lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;

            PopulateAssignedCategoryData(_context, Book);

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Author.Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName }),
                "ID",
                "FullName"
            );
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookToUpdate = await _context.Book
                .Include(i => i.Publisher)
                .Include(i => i.Author)
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }


            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.AuthorID,
                i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care 
            //este editata  
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }
    }
}
    
