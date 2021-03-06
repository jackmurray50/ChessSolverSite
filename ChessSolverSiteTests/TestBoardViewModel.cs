using System;
using Xunit;
using chess_solver_site.Models;

namespace ChessSolverSiteTests
{
    public class TestBoardViewModel
    {
        [Fact]
        public void Test_Add()
        {
            BoardViewModel bm = new BoardViewModel();
            bm.Turn = "BLACK";
            bm.BoardState = "Lah lah lah";
            bm.TurnsSinceCapture = 9;
            bm.WinState = "TBD";
            bm.Add();
            Assert.True(bm.Id > 0);
        }
        [Fact]
        public void Test_GetById()
        {

        }
    }
}
