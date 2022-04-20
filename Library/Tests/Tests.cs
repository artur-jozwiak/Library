using FluentAssertions;
using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using Library.BussinesLogic.Services;
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
    public class Tests 
    {
       private static LibraryContext context= new LibraryContext();
       private static IRepository<Book, int> _bookRepository = new BookRepository(context);
        private static IRepository<User, int> _userRepository = new UserRepository(context);
        private static IRepository<Order, int> _orderRepository = new OrderRepository(context);
        LibraryService libraryService = new LibraryService(_bookRepository, _userRepository, _orderRepository);
   
        [Fact]
        public async Task AddOrderTest()
        {

            using (var context = new LibraryContext())
            { 
                OrderRepository orderRepository = new OrderRepository(context);
                var order = new Order
                {
                    User = new User {Name="Tom", Surname="Stanley", PersonalNumber=123456, Role= Library.BussinesLogic.Enums.Role.Student },           
                    Book = new Book { Author= "Jarosław Grzędowicz", Category= Library.BussinesLogic.Enums.BookCategory.Fantasy, Quantity=3, Title="Pan Lodowego Ogrodu"},
                    BorrowInterval=56,
                    Cost=0           
                };
                orderRepository.Create(order);
                await context.SaveChangesAsync();
            }

            using (var context = new LibraryContext())
            {
                var orders = await context.Orders.ToArrayAsync();
                orders.Count().Should().Be(6);
                
            }
        }

        [Fact]
        public async Task AddBookTest()
        {
            using (var context = new LibraryContext())
            {
                var book = new Book
                {                   
                    Title = "Wiedźmin",
                    Author = "Andrzej Sapkowski",
                    Category= Library.BussinesLogic.Enums.BookCategory.Fantasy,
                    Quantity=3,  
                };
                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();
            }

            using (var context = new LibraryContext())
            {
                var books = await context.Books.ToArrayAsync();
                books.Count().Should().Be(4);
            }
        }
        [Fact]
        public async Task DeleteOrderTest()
        {
            using (var context = new LibraryContext())
            {
                OrderRepository orderRepository = new OrderRepository(context);
                var order = new Order
                {
                    User = new User { Name = "Arkadiusz", Surname = "Ostrowski", PersonalNumber = 123823456, Role = Library.BussinesLogic.Enums.Role.Student },
                    Book = new Book { Author = "Andrzej Ziemiański", Category = Library.BussinesLogic.Enums.BookCategory.Fantasy, Quantity = 3, Title = "Achaja" },
                    BorrowInterval = 56,
                    Cost = 0
                };
                orderRepository.Create(order);
                await context.SaveChangesAsync();

                int orderId = 6;
                orderRepository.Delete(orderId);
                await context.SaveChangesAsync();
            }

            using (var context = new LibraryContext())
            {
                var orders = await context.Orders.ToArrayAsync();
                 orders.Count().Should().Be(5);

            }
        }

        [Fact]
        public async Task AddUserTest()
        {  
            using (var context = new LibraryContext())
            {
                var user = new User
                {
                    Name = "Karolina",
                    Surname = "Janowska",
                    PersonalNumber = 1234567899,
                    Role = Library.BussinesLogic.Enums.Role.Employee,
                    
                };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }

            using (var context = new LibraryContext())
            {
                var users = await context.Users.ToArrayAsync();
                users.Count().Should().Be(4);
            }
        }

        [Fact]
        public async Task BorrowABookTest()
        {
      
            using ( context = new LibraryContext())
            {
                int bookId = 2;
                libraryService.BorrowABook(bookId,2);
                await context.SaveChangesAsync();
                
                //var book =context.Books.First();
                var book = context.Books.FirstOrDefault(b=>b.Id == bookId);
                book.Quantity.Should().Be(2);
            }

        }

        [Fact]
        public async Task GiveBackBookTest()
        {
            using (context = new LibraryContext())
            {
                int orderId = 3; 
                var order = context.Orders.FirstOrDefault(o=>o.Id==orderId);
                libraryService.GiveBackABook(order.Id);
                await context.SaveChangesAsync();
                var book = context.Books.FirstOrDefault(b=>b.Id==order.BookId);
                book.Quantity.Should().Be(1);
            }

        }

        [Fact]
       public async Task GetCostOfOrderForLecturerTest()
        {
            int borrowInterval = 14;
            float cost= libraryService.GetCostOfOrderForLecturer(borrowInterval);
            cost.Should().Be(22);
        }

        [Fact]
        public async Task GetCostOfOrderForstudentTest()
        {
            int borrowInterval = 16;
            float cost = libraryService.GetCostOfOrderForStudent(borrowInterval);
            cost.Should().Be(31);
        }
        [Fact]
        public async Task GetCostOfOrderForEmployeeTest()
        {
            int borrowInterval = 30;
            float cost = libraryService.GetCostOfOrderForEmployee(borrowInterval);
            cost.Should().Be(10);
        }

        [Fact]
        public async  Task GetCostOfOrderTest()
        {
            int  orderId = 3;
            var order =  context.Orders.FirstOrDefault(o => o.Id == orderId);
            float cost = libraryService.GetCostOfOrder(order);

            cost.Should().Be(0);
        }

        [Fact]
        public async Task GetCostOfOrderWithDates()
        {
            DateTime startTime= DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(32);
            int orderId = 1;
            var order = context.Orders.FirstOrDefault(o => o.Id == orderId);
            float cost = libraryService.GetCostOfOrder(order,startTime,endTime);

            cost.Should().Be(131);
        }
    }
}
