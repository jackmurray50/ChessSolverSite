using System;
using Xunit;
using chess_solver_site.Models;

namespace ChessSolverSiteTests
{
    public class TestAccountViewModel
    {
        [Fact]
        public void Test_GetById()
        {
            AccountViewModel vm = new AccountViewModel();
            vm.Id = 2;
            vm.GetById();
            Assert.NotNull(vm.Name);
        }

        [Fact]
        public void Test_Add()
        {
            AccountViewModel vm = new AccountViewModel();
            vm.Name = "Kaitlyn";
            vm.Password = "Adair";
            vm.Progress = 8;
            vm.Add();
            Assert.True(vm.Id > 0);
        }
    }
}
