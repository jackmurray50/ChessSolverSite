using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace chess_solver_site.Models
{
    public class ChessSolverRepository<T> : IRepository<T> where T : ChessSolverEntity
    {
        private ChessSolverContext _db = null;

        public ChessSolverRepository(ChessSolverContext context = null)
        {
            _db = context != null ? context : new ChessSolverContext();
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public List<T> GetByExpression(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Where(match).ToList();
        }

        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                ChessSolverEntity curEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                _db.Entry(curEntity).CurrentValues.SetValues(updatedEntity);

                if (_db.SaveChanges() == 1)
                {
                    opStatus = UpdateStatus.Ok;
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException dbx)
            {
                opStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);
            }
            return opStatus;
        }
        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            _db.Set<T>().Remove(currentEntity);
            return _db.SaveChanges();
        }
    }
}
