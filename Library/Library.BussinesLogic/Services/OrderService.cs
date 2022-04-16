using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BussinesLogic.Services
{
    public class OrderService
    {
        private readonly IRepository<Book, int> _bookRepository;
        private readonly IRepository<User, int> _userRepository;
        private readonly IRepository<Order, int> _orderRepository;
        public OrderService(IRepository<Book, int> bookRepository, IRepository<User, int> userRepository, IRepository<Order, int> orderRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public void BorrowABook()
        {
            Order order = new Order();
            User user = order.User;
            Book book = order.Book;

            if (order.Book.Quantity > 0)
            {                    
                book.Quantity--;
                user.Books.Add(order.Book);
                _orderRepository.Create(order);
                _orderRepository.Save();
            }
        }
        public void BorrowABook(int bookId, int userId)
        {
            Order order = new Order();
            User user = order.User;
            Book book = order.Book;

            order.Book = _bookRepository.GetById(bookId);
            order.User = _userRepository.GetById(userId);

            if (order.Book.Quantity > 0)
            {
                book.Quantity--;
                user.Books.Add(order.Book);
                _orderRepository.Create(order);
                _orderRepository.Save();
            }
        }

        public void GiveBackABook(Order order)
        { 
            Book book = order.Book;
            User user = order.User;

            user.Books.Remove(book);
            book.Quantity++;
            _orderRepository.Delete(order.Id);
            _orderRepository.Save();
        }

        public float GetCostOfOrder(int rentInterval )
        {
            float cost = 0;
            return cost;
        }

        public int GetRentInterval(DateTime startTime, DateTime endTime)
        {
            TimeSpan borrowInterval = startTime - endTime;
            int intBorrowInterval = (int)borrowInterval.TotalDays;

            return intBorrowInterval;
        }

        






    }
}
