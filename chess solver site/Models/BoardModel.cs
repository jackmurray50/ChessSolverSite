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

        public int Add(Boards newBoard)
        {
            try
            {
                repository.Add(newBoard);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newBoard.Id;
        }//end of Add

        public UpdateStatus Update(Boards newBoard)
        {
            UpdateStatus us = UpdateStatus.Failed;
            try
            {
                us = repository.Update(newBoard);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                us = UpdateStatus.Stale;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return us;
        }

        public Boards GetById(int id)
        {
            List<Boards> selectedBoard = null;
            try
            {
                selectedBoard = repository.GetByExpression(b => b.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedBoard.FirstOrDefault();
        }

        public Boards GetByBoardState(string state)
        {
            List<Boards> selectedBoard = null;
            try
            {
                selectedBoard = repository.GetByExpression(b => b.BoardState == state);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedBoard.FirstOrDefault();
        }

        public int Delete(int id)
        {
            int boardsDeleted = -1;
            try
            {
                boardsDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return boardsDeleted;
        }
    }
}
