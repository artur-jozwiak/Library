using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BussinesLogic.Services
{
    public class LibraryService : ILibraryService
    {
        private  IRepository<Book, int> _bookRepository;
        private readonly IRepository<User, int> _userRepository;
        private readonly IRepository<Order, int> _orderRepository;
        public LibraryService(IRepository<Book, int> bookRepository, IRepository<User, int> userRepository, IRepository<Order, int> orderRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }
 
        public void BorrowABook(int bookId, int userId)
        {
            Order order = new Order();     
            var book= _bookRepository.GetById(bookId);
            var user = _userRepository.GetById(userId);

            order.User = user;
            order.Book = book;

            if (order.Book.Quantity > 0)
            {
                order.Book.Quantity--;   
                _orderRepository.Create(order);
                _orderRepository.Save();
            }
        }

        public void GiveBackABook(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
           
            _bookRepository.GetAll();
            _userRepository.GetAll();
            int bookId = order.Book.Id;
            int userId = order.User.Id;
            var book = _bookRepository.GetById(bookId);
            var user = _userRepository.GetById(userId);

            book.Quantity++;
            _orderRepository.Delete(order.Id);
            _orderRepository.Save();
        }

        public float GetCostOfOrder(Order order)
        {  
            int userId = order.UserId;
            var user = _userRepository.GetById(userId);

            float cost = 0;
            switch(user.Role)
            {
                case Enums.Role.Lecturer:
                    cost = GetCostOfOrderForLecturer(order.BorrowInterval);
                 break;
                case Enums.Role.Student:
                    cost = GetCostOfOrderForStudent(order.BorrowInterval);
                 break;
                case Enums.Role.Employee:
                    cost=GetCostOfOrderForEmployee(order.BorrowInterval);
                    break;
                default:
                    break;
            }

            order.Cost = cost;     
            return cost;
        }
        public float GetCostOfOrder(Order order, DateTime startTime, DateTime endTime)
        {
            order.BorrowInterval = GetBorrowInterval(startTime, endTime);
            int userId = order.UserId;
            var user = _userRepository.GetById(userId);

            float cost = 0;
            switch (user.Role)
            {
                case Enums.Role.Lecturer:
                    cost = GetCostOfOrderForLecturer(order.BorrowInterval);
                    break;
                case Enums.Role.Student:
                    cost = GetCostOfOrderForStudent(order.BorrowInterval);
                    break;
                case Enums.Role.Employee:
                    cost = GetCostOfOrderForEmployee(order.BorrowInterval);
                    break;
                default:
                    break;
            }

            order.Cost = cost;
            return cost;
        }


        public float GetCostOfOrderForLecturer(int borrowInterval)
        {
            float cost = new float();

            if (borrowInterval > 28)
            {
                cost = (borrowInterval - 28) * 10 + (28 - 14) * 5 + (14 - 3) * 2;
            }
            else if (borrowInterval <= 28 && borrowInterval > 14)
            {
                cost = (borrowInterval - 14) * 5 + (14 - 3) * 2;
            }
            else if (borrowInterval <= 14 && borrowInterval > 3)
            {
                cost = (borrowInterval - 3) * 2;
            }
            else if (borrowInterval <= 3)
            {
                cost = 0;
            }
            return cost;
        }


        public float GetCostOfOrderForStudent(int borrowInterval)
        {
            float cost = new float();

            if (borrowInterval > 28)
            {
                cost = (borrowInterval - 28) * 10 + (28 - 14) * 5 + (14 - 7) * 2 + 7;
            }
            else if (borrowInterval <= 28 && borrowInterval > 14)
            {
                cost = (borrowInterval - 14) * 5 + (14 - 7) * 2 + 7;
            }
            else if (borrowInterval <= 14 && borrowInterval > 7)
            {
                cost = (borrowInterval - 7) * 2 +7;
            }
            else if (borrowInterval <= 7)
            {
                cost = borrowInterval *1;
            }
            return cost;
        }
        public float GetCostOfOrderForEmployee(int borrowInterval)
        {
            float cost = new float();

            if (borrowInterval > 28)
            {
                cost = (borrowInterval - 28) * 5;
            }

            else if (borrowInterval <= 28)
            {
                cost = 0;
            }
            return cost;
        }

        public int GetBorrowInterval(DateTime startTime, DateTime endTime)
        {
            TimeSpan borrowInterval = endTime-startTime;
            int intBorrowInterval = (int)borrowInterval.TotalDays;
            return intBorrowInterval;
        }
    }
}
