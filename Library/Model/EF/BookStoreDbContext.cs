using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Library.Model.EF
{
   public class BookStoreDbContext:DbContext
    {
        public  BookStoreDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<User> User { get; set; }
    }
}
