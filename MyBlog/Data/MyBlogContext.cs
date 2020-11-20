using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.Entity;


namespace MyBlog.Data
{
    public class MyBlogContext : IdentityDbContext
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPost>()
                .HasOne<User>(u => u.User)
                .WithMany(p => p.BlogPosts)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<BlogPostClaps>()
                .HasKey(bc => new { bc.BlogPostId, bc.ClapId });
        }

        public DbSet<User> User { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostClaps> BlogPostClaps { get; set; }
        public DbSet<Claps> Claps { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Following> Followings { get; set; }

       

    }
}
