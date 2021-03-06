using System;
using Xunit;
using chess_solver_site.Models;
using chess_solver_site;

namespace ChessSolverSiteTests
{
    public class TestBoardModel
    {
        BoardModel bm = new BoardModel();
        [Fact]
        public void Test_Add()
        {
            Boards newBoard = new Boards();
            newBoard.BoardState = "rhbqkbhr\npppppppp\nX#X#X#X#\nX#X#X#X#\n" +
                "X#X#X#X#\nX#X#X#X#\nPPPPPPPP\nRHBQKBHR";
            newBoard.Turn = "BLACK";
            newBoard.WinState = "NA";
            newBoard.TurnsSinceCapture = 0;

            bm.Add(newBoard);
        }
        [Fact]
        public void Test_Update()
        {
            Boards newBoard = bm.GetById(1);
            newBoard.BoardState = "Updated";

            Assert.True(bm.Update(newBoard) == UpdateStatus.Ok);
        }

        [Fact]
        public void Test_GetByID()
        {
            bm.GetById(1);
        }

        [Fact]
        public void Test_GetByBoardState()
        {
            bm.GetByBoardState("Updated", "Black");
        }
        [Fact]
        public void Test_Delete()
        {
            Assert.True(bm.Delete(1) == 1);
        }
    }
}
