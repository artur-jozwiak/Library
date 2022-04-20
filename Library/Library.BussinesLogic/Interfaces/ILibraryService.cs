using Library.BussinesLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BussinesLogic.Interfaces
{
    public interface ILibraryService
    {
        void BorrowABook(int bookId, int userId);
        void GiveBackABook(int orderId);
        float GetCostOfOrder(Order order);
        float GetCostOfOrder(Order order, DateTime startTime, DateTime endTime);
        float GetCostOfOrderForLecturer(int borrowInterval);
        float GetCostOfOrderForStudent(int borrowInterval);
        float GetCostOfOrderForEmployee(int borrowInterval);
        int GetBorrowInterval(DateTime startTime, DateTime endTime);

    }
}
