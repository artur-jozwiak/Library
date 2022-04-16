using FluentAssertions;
using Library.BussinesLogic.Models;
using Library.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    [Trait("Category", "InMemory")]
    public class Tests
    {
        [Fact]
        public async Task AddOrder()
        {
            // adding entry
            using (var context = new LibraryContext())
            {
                var order = new Order
                {
                    
                    //UserId = 1,
                    User = new User { Books = new List<Book>(), /*Id=1, */Name="Tom", Surname="Stanley", PersonalNumber=123456, Role= Library.BussinesLogic.Enums.Role.Student },
                    //BookId = 1,
                    Book = new Book {/* Id = 1,*/ Author= "Jarosław Grzędowicz", Category= Library.BussinesLogic.Enums.BookCategory.Fantasy, Quantity=3, Title="Pan Lodowego Ogrodu"},
                    BorrowInterval=56,
                    Cost=0           
                };

                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

            }

            // checking if entry was inserted to database
            using (var context = new LibraryContext())
            {
                var orders = await context.Orders/*.Where(m => m.Id == 1)*/.ToArrayAsync();

                orders.Count().Should().Be(4);
                //var order = orders.First();
                //order.Book.Author.Should().Be("Hary Potter");
            }
        }

        [Fact]
        public async Task AddBook()
        {
            // adding entry
            using (var context = new LibraryContext())
            {
                var book = new Book
                {
                   
                    Title = "Metro",
                    Author = "Dimitry Gluchowsky",
                    Category= Library.BussinesLogic.Enums.BookCategory.Fantasy,
                    Quantity=3,  
                };
                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();
            }

            // checking if entry was inserted to database
            using (var context = new LibraryContext())
            {
                var orders = await context.Books/*.Where(m => m.Id == 1)*/.ToArrayAsync();

                orders.Count().Should().Be(1);
                //var order = orders.First();
                //order.Book.Author.Should().Be("Hary Potter");
            }
        }
    }
}
