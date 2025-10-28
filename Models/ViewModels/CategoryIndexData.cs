using System.Collections.Generic;
using topai_carina_ana_lab2.Models;

namespace topai_carina_ana_lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IList<Category> Categories { get; set; }
        public IEnumerable<Book>? Books { get; set; }
    }
}