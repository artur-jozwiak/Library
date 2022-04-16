using FluentAssertions;
using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using Library.Database;
using Library.Database.Repositories;
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
    public class Tests /*: IClassFixture<OrderRepository>*/
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

                orders.Count().Should().Be(8);
                //var order = orders.First();
                //order.Book.Author.Should().Be("Hary Potter");
            }
        }

        [Fact]
        public async Task AddBook()
        {
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

            using (var context = new LibraryContext())
            {
                var books = await context.Books.ToArrayAsync();
                books.Count().Should().Be(19);
            }
        }

        [Fact]
        public async Task AddUser()
        {  
            using (var context = new LibraryContext())
            {
                var user = new User
                {
                    Name = "Karolina",
                    Surname = "Kowalska",
                    PersonalNumber = 1234567899,
                    Role = Library.BussinesLogic.Enums.Role.Employee,
                    Books = new List<Book>()
                };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }

            using (var context = new LibraryContext())
            {
                var users = await context.Users.ToArrayAsync();
                users.Count().Should().Be(7);
            }
        }


        [Fact]
        public async Task BorrowABook()
        {

            using (var context = new LibraryContext())
            {
                var order = new Order
                {
                    User = context.Users.First(),
                    Book = context.Books.First(),
                    BorrowInterval = 56,
                    Cost = 0
                };
                var book = order.Book;
                order.Book.Quantity--;
                order.User.Books.Add(book);
             
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

                var orders = await context.Orders.ToArrayAsync(); 
                //order.Book.Quantity.Should().Be(-3);
                order.User.Books.Count().Should().Be(1);
                
            }

            //using (var context = new LibraryContext())
            //{
            //    var orders = await context.Orders.ToArrayAsync();
            //     context.Orders.Last().Book.Quantity.Should().Be(2);
            //    //orders.Count().Should().Be(6);
            //}
        }
    }
}
