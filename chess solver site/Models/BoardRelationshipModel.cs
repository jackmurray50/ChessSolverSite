using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace chess_solver_site.Models
{
    public class BoardRelationshipModel : ChessSolverEntity
    {
        IRepository<BoardsRelationships> repository;

        public BoardRelationshipModel()
        {
            repository = new ChessSolverRepository<BoardsRelationships>();
        }

        public List<BoardsRelationships> GetAllParentsOfId(int id)
        {
            List<BoardsRelationships> selectedBr = null;
            try
            {
                selectedBr = repository.GetByExpression(b => b.ParentId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedBr;
        }
        public List<BoardsRelationships> GetAllChildrenOfId(int id)
        {
            List<BoardsRelationships> selectedBr = null;
            try
            {
                selectedBr = repository.GetByExpression(b => b.ChildId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedBr;
        }
    
        public BoardsRelationships GetByParentAndChild(int parentId, int childId)
        {
            List<BoardsRelationships> selectedBr = null;
            try
            {
                selectedBr = repository.GetByExpression(b => b.ChildId == childId && b.ParentId == parentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedBr.FirstOrDefault();
        }
        public int Add(BoardsRelationships br)
        {
            try
            {
                repository.Add(br);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return br.Id;
        }
    
        public int Delete(int id)
        {
            int BoardsRelationshipsDeleted = -1;
            try
            {
                BoardsRelationshipsDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return BoardsRelationshipsDeleted;
        }
    
        
    }
}
