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
    public class Tests  /*:IClassFixture<IRepository<Book,int>>*/
    {
        //private IRepository<Book, int> _bookRepository;
        //private readonly IRepository<User, int> _userRepository;
        //private readonly IRepository<Order, int> _orderRepository;


       private static LibraryContext context= new LibraryContext();
       private static IRepository<Book, int> _bookRepository = new BookRepository(context);
        private static IRepository<User, int> _userRepository = new UserRepository(context);
        private static IRepository<Order, int> _orderRepository = new OrderRepository(context);

        OrderService orderService = new OrderService(_bookRepository, _userRepository, _orderRepository);
        //public Tests(IRepository<Book, int> bookRepository/*, IRepository<User, int> userRepository, IRepository<Order, int> orderRepository*/)
        //{
        //    _bookRepository = bookRepository;
        //    //_userRepository = userRepository;
        //    //_orderRepository = orderRepository;
        //}

        [Fact]
        public async Task AddOrderTest()
        {
            // adding entry
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
                //await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
            // checking if entry was inserted to database
            using (var context = new LibraryContext())
            {
                var orders = await context.Orders/*.Where(m => m.Id == 1)*/.ToArrayAsync();

                orders.Count().Should().Be(9);
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
        public async Task BorrowABookTest()
        {
            //var context = new LibraryContext();
            //IRepository<Book, int> _bookRepository = new BookRepository(context);
            //IRepository<User, int> _userRepository = new UserRepository(context);
            //IRepository<Order, int> _orderRepository = new OrderRepository(context);

            //OrderService orderService = new OrderService(_bookRepository, _userRepository, _orderRepository);
            using ( context = new LibraryContext())
            {
                int bookId = 2;
                orderService.BorrowABook(bookId,2);
                await context.SaveChangesAsync();
                
                //var book =context.Books.First();
                var book = context.Books.FirstOrDefault(b=>b.Id == bookId);
                book.Quantity.Should().Be(1);
            }

        }

        [Fact]
        public async Task GiveBackBookTest()
        {
            using (context = new LibraryContext())
            {
                int orderId = 5; 
                var order = context.Orders.FirstOrDefault(o=>o.Id==orderId);
                orderService.GiveBackABook(order.Id);
                await context.SaveChangesAsync();
                var book = context.Books.FirstOrDefault(b=>b.Id==order.BookId);
                book.Quantity.Should().Be(4);
            }

        }

        [Fact]
       public async Task GetCostOfOrderForLecturerTest()
        {
            float cost= orderService.GetCostOfOrderForLecturer(14);
            cost.Should().Be(22);
        }

        [Fact]
        public async Task GetCostOfOrderForstudentTest()
        {
            float cost = orderService.GetCostOfOrderForStudent(16);
            cost.Should().Be(31);
        }
        [Fact]
        public async Task GetCostOfOrderForEmployeeTest()
        {
            float cost = orderService.GetCostOfOrderForEmployee(30);
            cost.Should().Be(10);
        }

        [Fact]
        public async  Task GetCostOfOrderTest()
        {
            int  orderId = 3;
            var order =  context.Orders.FirstOrDefault(o => o.Id == orderId);
            float cost = orderService.GetCostOfOrder(order);

            cost.Should().Be(0);
        }

        [Fact]
        public async Task GetCostOfOrderWithDates()
        {
            DateTime startTime= DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(32);
            int orderId = 1;
            var order = context.Orders.FirstOrDefault(o => o.Id == orderId);
            float cost = orderService.GetCostOfOrder(order,startTime,endTime);

            cost.Should().Be(131);
        }
    }
}
