using Library.BussinesLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BussinesLogic.Models
{
    public class Book
    {
       
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public BookCategory Category { get; set; }
            public int Quantity { get; set; }
            
       
    }
}
