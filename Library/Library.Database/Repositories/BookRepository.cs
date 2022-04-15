using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Database.Repositories
{
    public class BookRepository : IRepository <Book, int>
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public Book Create(Book entity)
        {
            _context.Books.Add(entity);
            return entity;

        }

        public void Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.FirstOrDefault(u => u.Id == id);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }

        public void Update(Book entity)
        {
            
            _context.Update(entity);

        }
    }
}
