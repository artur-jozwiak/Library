using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Database.Repositories
{
    public class OrderRepository : IRepository<Order, int>
    {
        private readonly LibraryContext _context;
        public OrderRepository(LibraryContext context)
        {
            _context = context;
        }

        

        public Order Create(Order entity)
        {
            if (entity.Book.Quantity > 0)
            {
                entity.Book.Quantity--;
                _context.Orders.Add(entity);
                return entity;
            }
            return null;
        }

        public void Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(u => u.Id == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
     

        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(u => u.Id == id);
        }

        public void Update(Order entity)
        {
            _context.Update(entity);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }

       
    }
}
