using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Database.Repositories
{
    public class UserRepository : IRepository<User, int>

    {
        private readonly LibraryContext _context;
        public UserRepository(LibraryContext context)
        {
            _context = context;
        }

        public User Create(User entity)
        {
            _context.Users.Add(entity);
            return entity;  
        }

        public void Delete(int id)
        {
            var user =  _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u=>u.Id==id);
        }

        public  void Save()
        {
             _context.SaveChangesAsync();
        }

        public void Update(User entity)
        {          
            _context.Update(entity);          
        }
    }
}
