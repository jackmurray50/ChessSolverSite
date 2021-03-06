using System;
using Xunit;
using chess_solver_site.Models;
using chess_solver_site; 

namespace ChessSolverSiteTests
{
    public class TestAccountModel
    {
        AccountModel am = new AccountModel();
        [Fact]
        public void Test_Add()
        {
            Accounts acc = new Accounts();
            acc.Name = "Jack";
            acc.Password = "InsecurePassword";
            acc.Progress = 1;

            am.Add(acc);
        }
        [Fact]
        public void Test_Update()
        {
            Accounts acc = am.GetAccountByName("Jack");
            acc.Progress++;
            am.Update(acc);
        }

        [Fact]
        public void Test_Delete()
        {
            Assert.True(am.Delete(1) == 1);
        }
    
        [Fact]
        public void Test_GetById()
        {
            am.GetAccountByID(1);
        }
        [Fact]
        public void Test_GetByName()
        {
            am.GetAccountByName("Jack");
        }
    }
}
