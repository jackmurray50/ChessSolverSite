using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace chess_solver_site.Models
{
    public class AccountModel
    {
        IRepository<Accounts> repository;

        public AccountModel()
        {
            repository = new ChessSolverRepository<Accounts>();
        }

        public Accounts GetByID(int id)
        {
            List<Accounts> selectedAccount = null;
            try
            {
                selectedAccount = repository.GetByExpression(a => a.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedAccount.FirstOrDefault();
        }
        public Accounts GetByName(string name)
        {
            List<Accounts> selectedAccount = null;
            try
            {
                selectedAccount = repository.GetByExpression(a => a.Name == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedAccount.FirstOrDefault();
        }
        
        public int Add(Accounts account)
        {
            try
            {
                repository.Add(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return account.Id;
        }
    
        public UpdateStatus Update(Accounts account)
        {
            UpdateStatus us = UpdateStatus.Failed;
            try
            {
                us = repository.Update(account);
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

        public int Delete(int id)
        {
            int accountsDeleted = -1;
            try
            {
                accountsDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return accountsDeleted;
        }
    }
}
