using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoungStartUp.Models;

namespace YoungStartUp.Controllers
{
    public class YoungStartUpDbContext :DbContext
    {
        public YoungStartUpDbContext(DbContextOptions<YoungStartUpDbContext>options ): base (options)
        {

        }
        public DbSet<LogInUser> LogInUser { get; set; }
        public DbSet<Admin> Admin { get; set; }//table admin in DB
        public DbSet<Project> Project { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
