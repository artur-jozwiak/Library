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

            optionsBuilder
                .UseSqlServer("Server=localhost;Database=LibraryDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var order = modelBuilder.Entity<Order>();
            var user = modelBuilder.Entity<User>();
            var book =modelBuilder.Entity<Book>();

                order.HasOne(o => o.Book)
                .WithMany()
                .HasForeignKey(o => o.BookId)
                .IsRequired();
           
                 order.HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o=>o.UserId)
                .IsRequired();
           
                user.HasIndex(u => u.PersonalNumber)
                .IsUnique();
            
                user.Property(u => u.PersonalNumber)
                .HasMaxLength(11);
           
                user.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
           
                 user.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(50);
            
               user.Property(u => u.Role)
               .IsRequired();

                book.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);

                book.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(50);

                book.Property(b => b.Category)
                .IsRequired();
        }
    }
}
