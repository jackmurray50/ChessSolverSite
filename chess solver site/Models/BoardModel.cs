using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace chess_solver_site.Models
{
    public class BoardModel
    {
        IRepository<Boards> repository;

        public BoardModel()
        {
            repository = new ChessSolverRepository<Boards>();   
        }

        public Boards GetById(int id)
        {
            List<Boards> selectedStudent = null;
            try
            {
                selectedStudent = repository.GetByExpression(stu => stu.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedStudent.FirstOrDefault();
        }
    }
}
