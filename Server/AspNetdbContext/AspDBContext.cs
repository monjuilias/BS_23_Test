using AspNet.DTO;
using AspNet.DTO.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetdbContext
{
    public class AspDBContext : DbContext
    {
        public AspDBContext( DbContextOptions<AspDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>(entity => {
                entity.HasKey(u => u.UserID);

            });

            modelBuilder.Entity<Post>(entity => {
                entity.HasKey(p => p.PostID);
                entity.Ignore(c => c.NumberOfComments);


            });
            modelBuilder.Entity<Vote>(entity => {
                entity.HasKey(v => v.VoteID);
                entity.HasOne(c => c.Comments)
                    .WithMany(p => p.Vote)
                    .HasForeignKey(d => d.CommentsID);
            });
            modelBuilder.Entity<Comments>(entity => {
                entity.HasKey(e => e.CommentsID);
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostID);
                entity.Ignore(d => d.NumberOfDisLike);
                entity.Ignore(d => d.NumberOfLike);

            });
        }
    }
}
