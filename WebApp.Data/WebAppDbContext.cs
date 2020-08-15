using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core;

namespace WebApp.Data
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) 
            : base(options) 
        {

        }

        public DbSet<Fyle> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<BasePath> BasePath { get; set; }

    }
}
