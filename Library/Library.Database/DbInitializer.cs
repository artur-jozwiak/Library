using Library.BussinesLogic.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Database
{
    public class DbInitializer
    {


        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();
        }

        public static void Initialize( IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryContext>();

                if(!context.Books.Any())
                {
                    context.Books.AddRange(
                    new Book()
                    {
                        Title = "Clean Code",
                        Author = "Robert Cecil Martin",
                        Category = BussinesLogic.Enums.BookCategory.Education,
                        Quantity = 2
                    },
                    new Book()
                    {
                        Title = "Pan Lodowego Ogrodu",
                        Author = "Jarosław Grzędowicz",
                        Category = BussinesLogic.Enums.BookCategory.Fantasy,
                        Quantity = 4
                    },
                    new Book()
                    {
                        Title = "Metro 2033",
                        Author = "Dmitrij Głuchowski",
                        Category = BussinesLogic.Enums.BookCategory.Fantasy,
                        Quantity = 1
                    });
                    context.SaveChanges();

                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                    new User()
                    {
                        Name = "Natalia",
                        Surname = "Sosnowska",
                        PersonalNumber = 59345678912,
                        Role = BussinesLogic.Enums.Role.Employee
                    },
                    new User()
                    {
                        Name = "Adam",
                        Surname = "Kowalski",
                        PersonalNumber = 98765432198,
                        Role = BussinesLogic.Enums.Role.Student
                    },
                    new User()
                    {
                        Name = "Jan",
                        Surname = "Malinowski",
                        PersonalNumber = 65478932178,
                        Role = BussinesLogic.Enums.Role.Employee
                    });
                    context.SaveChanges();

                }

                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(
                    new Order()
                    {
                        User = context.Users.FirstOrDefault(u => u.Id == 1),
                        Book = context.Books.FirstOrDefault(u => u.Id == 1),
                        BorrowInterval = 30,
                        Cost = 0
                    },
                    new Order()
                    {
                        User = context.Users.FirstOrDefault(u => u.Id == 2),
                        Book = context.Books.FirstOrDefault(u => u.Id == 2),
                        BorrowInterval = 30,
                        Cost = 0
                    },
                    new Order()
                    {
                        User = context.Users.FirstOrDefault(u => u.Id == 3),
                        Book = context.Books.FirstOrDefault(u => u.Id == 3),
                        BorrowInterval = 30,
                        Cost = 0
                    });
                    context.SaveChanges();

                }
            }
        }
    }
}
