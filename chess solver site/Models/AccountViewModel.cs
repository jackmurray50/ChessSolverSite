using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace chess_solver_site.Models
{
    public class AccountViewModel
    {
        private AccountModel _model;

        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Progress { get; set; }

        public AccountViewModel()
        {
            _model = new AccountModel();
        }

        public void GetById()
        {
            try
            {
                Accounts acc = _model.GetByID(Id);

                Id = acc.Id;
                Password = acc.Password;
                Name = acc.Name;
                Progress = acc.Progress;
            }
            catch (NullReferenceException)
            {
                Name = "not found";
            }
            catch (Exception ex)
            {
                Name = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public void GetByName()
        {
            try
            {
                Accounts acc = _model.GetByName(Name);

                Id = acc.Id;
                Password = acc.Password;
                Name = acc.Name;
                Progress = acc.Progress;
            }
            catch (NullReferenceException)
            {
                Name = "not found";
            }
            catch (Exception ex)
            {
                Name = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    
        public void Add()
        {
            try
            {
                Id = -1;
                Accounts acc = new Accounts();
                acc.Name = Name;
                acc.Password = Password;
                acc.Progress = Progress;
                Id = _model.Add(acc);
            }catch(Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

        }

        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                Accounts acc = new Accounts();
                acc.Name = Name;
                acc.Id = Id;
                acc.Password = Password;
                acc.Progress = Progress;
                opStatus = _model.Update(acc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus);
        }
    
        public int Delete()
        {
            int accountDeleted = -1;

            try
            {
                accountDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return accountDeleted;
        }
    }
}
