using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace chess_solver_site
{
    interface IRepository<T>
    {
        List<T> GetAll();

        List<T> GetByExpression(Expression<Func<T, bool>> match);
        T Add(T entity);
        UpdateStatus Update(T entity);
        int Delete(int i);
    }
}
