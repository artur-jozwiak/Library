
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


//using (var context = new LibraryContext())
//{
//    var book = new Book
//    {
//        Title = "Hary Potter",
//        Author = " J.K. Rowling",
//        Category = Library.BussinesLogic.Enums.BookCategory.SciFi,
//        Quantity = 3

//    };
//    new Book
//    {
//        Title = "Clean Code",
//        Author = "Robert Cecil Martin",
//        Category = Library.BussinesLogic.Enums.BookCategory.Fantasy,
//        Quantity = 2

//    };
//    new Book
//    {
//        Title = "Metro",
//        Author = "Dimitry Gluhovsky",
//        Category = Library.BussinesLogic.Enums.BookCategory.SciFi,
//        Quantity = 2

//    };

//    await context.AddAsync(book);
//    await context.SaveChangesAsync();




//    //var books = await context.Books.ToArrayAsync();
//    var user = new User
//    {
//        Name = "Marcin",
//        Surname = "Kowalski",
//        PersonalNumber = 5121376,
//        Role = Library.BussinesLogic.Enums.Role.Employee
//    };
//    await context.AddAsync(user);
//    await context.SaveChangesAsync();

//    var order = new Order
//    {
//        Book = book,
//        User = user,
//        UserId = 1,
//        BookId = 3,
//        StartTime = DateTime.Now,
//        EndTime = DateTime.Now,
//        RentInterval = 3,
//        Cost = 0

//    };
//    await context.AddAsync(order);
//    await context.SaveChangesAsync();
//}





namespace Library
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            
            string lecturer = "lecturer";
            string student = "student";
            string employee = "employee";

            string role="";
            int days = 10;
            float cost;

            switch(role)
            {

                case "lecturer":
                   if(days>28)
                    {
                        int a = days - 28;
                        int b = a - 14;
                        int c = b - 3;
                       cost = a * 10 + b * 5 + c * 2;       
                    }
                   else if(days<28 && days>14)
                    {
                        int a = days - 14;
                        int b =a - 3;
                        cost = a*5 + b* 2;
                    }
                   else if(days<14 && days>3)
                    {
                        int a = days - 3;
                        cost = a * 2;
                    }
                   else if(days<=3)
                    {
                        cost = 0;
                    }


                    break;

                default:
                    break;
            }


        }
    }
}