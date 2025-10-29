using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using topai_carina_ana_lab2.Models;

namespace topai_carina_ana_lab2.Data
{
    public class topai_carina_ana_lab2Context : DbContext
    {
        public topai_carina_ana_lab2Context (DbContextOptions<topai_carina_ana_lab2Context> options)
            : base(options)
        {
        }

        public DbSet<topai_carina_ana_lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<topai_carina_ana_lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<topai_carina_ana_lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<topai_carina_ana_lab2.Models.Category> Category { get; set; } = default!;
        public DbSet<topai_carina_ana_lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<topai_carina_ana_lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
