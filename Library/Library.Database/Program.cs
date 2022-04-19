
using Library.BussinesLogic.Interfaces;
using Library.BussinesLogic.Models;
using Library.BussinesLogic.Services;
using Library.Database;
using Library.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//static void CreateDbIfNotExists(IHost host)
//{
//    using (var scope = host.Services.CreateScope())
//    {
//        var services = scope.ServiceProvider;
//        try
//        {
//            var context = services.GetRequiredService<LibraryContext>();
//            DbInitializer.Initialize(context);
//        }
//        catch (Exception ex)
//        {
//            var logger = services.GetRequiredService<ILogger<Program>>();
//            logger.LogError(ex, "An error occurred creating the DB.");
//        }
//    }
//}





using (var context = new LibraryContext())
{
    var book = new Book
    {
        Title = "Hary Potter",
        Author = " J.K. Rowling",
        UserId = 3,
        Category = Library.BussinesLogic.Enums.BookCategory.SciFi,
        Quantity = 3,

    };

    var user = new User
    {
        Name = "Marcin",
        Surname = "Kowalski",
        PersonalNumber = 5121376,

        Role = Library.BussinesLogic.Enums.Role.Employee

    };
    await context.AddAsync(user);
    await context.SaveChangesAsync();

    var order = new Order
    {
        Book = book,
        User = user,
        UserId = 1,
        BookId = 3,
        BorrowInterval = 3,
        Cost = 0

    };
    await context.AddAsync(order);
    await context.SaveChangesAsync();
}

