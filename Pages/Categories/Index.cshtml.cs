using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using topai_carina_ana_lab2.Data;
using topai_carina_ana_lab2.Models;
using topai_carina_ana_lab2.Models.ViewModels;

namespace topai_carina_ana_lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly topai_carina_ana_lab2Context _context;

        public IndexModel(topai_carina_ana_lab2Context context)
        {
            _context = context;
        }

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {

            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                   .ThenInclude(bc => bc.Book)
                   .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
            
            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                    .Where(c => c.ID == id.Value).Single();
                CategoryData.Books = category.BookCategories
                    .Select(bc => bc.Book)
                    .ToList();
               
            }
        }
    } 
}

