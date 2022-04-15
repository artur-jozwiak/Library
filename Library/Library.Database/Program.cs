
using Library.BussinesLogic.Models;
using Library.Database;
using Microsoft.EntityFrameworkCore;

using (var context = new LibraryContext())
{
    var book = new Book
    {
        Title = "Hary Potter",
        Author = " J.K. Rowling",
        Category = Library.BussinesLogic.Enums.BookCategory.SciFi,
        Quantity = 3

    };
    new Book
    {
        Title = "Clean Code",
        Author = "Robert Cecil Martin",
        Category = Library.BussinesLogic.Enums.BookCategory.Fantasy,
        Quantity = 2

    };
    new Book
    {
        Title = "Metro",
        Author = "Dimitry Gluhovsky",
        Category = Library.BussinesLogic.Enums.BookCategory.SciFi,
        Quantity = 2

    };

    await context.AddAsync(book);
    await context.SaveChangesAsync();




    //var books = await context.Books.ToArrayAsync();
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
        StartTime = DateTime.Now,
        EndTime = DateTime.Now,
        RentInterval = 3,
        Cost = 0

    };
    await context.AddAsync(order);
    await context.SaveChangesAsync();
}