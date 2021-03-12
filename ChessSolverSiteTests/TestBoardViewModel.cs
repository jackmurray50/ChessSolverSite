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
            bm.BoardState = "RHBQKBHR\nPPPPPPPP\nX#X#X#X#\nX#X#X#X#\n" +
                "X#X#X#X#\nX#X#X#X#\npppppppp\nrhbqkbhr";
            bm.TurnsSinceCapture = 0;
            bm.WinState = "TBD";
            bm.Add();
            Assert.True(bm.Id > 0);
        }
        [Fact]
        public void Test_GetById()
        {
            BoardViewModel bm = new BoardViewModel();
            bm.Id = 1;
            bm.GetById();
            Assert.True(bm.BoardState != null);
        }
    }
}
