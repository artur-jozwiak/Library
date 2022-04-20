using Library.BussinesLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Database
{
    public class LibraryContext:DbContext
    {

        public LibraryContext()
        {

        }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseLazyLoadingProxies();

            optionsBuilder
                .UseSqlServer("Server=localhost;Database=LibraryDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Book)
                .WithMany()
                .HasForeignKey(o => o.BookId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o=>o.UserId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PersonalNumber)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.PersonalNumber)
                .HasMaxLength(11);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
               .Property(u => u.Surname)
               .IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<User>()
               .Property(u => u.Role)
               .IsRequired();
              







        }
    }
}
