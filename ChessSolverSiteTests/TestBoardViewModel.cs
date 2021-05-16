using System;
using Xunit;
using chess_solver_site.Models;
using chess_solver_site;

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
            bm.WinState = "NA";
            bm.IsFinished = false;
            bm.Add();
            Assert.True(bm.Id > 0);
        }

        [Fact]
        public void Test_Update()
        {
            BoardViewModel bm = new BoardViewModel();
            bm.Id = 1;
            bm.GetById();
            bm.TurnsSinceCapture = 30;

            Assert.True(bm.Update() == Convert.ToInt16(UpdateStatus.Ok));
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
