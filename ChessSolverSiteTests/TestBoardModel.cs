using System;
using Xunit;
using chess_solver_site.Models;

namespace ChessSolverSiteTests
{
    public class TestBoardModel
    {
        BoardModel bm = new BoardModel();
        [Fact]
        public void Test_Add()
        {
            Boards newBoard = new Boards();
            newBoard.BoardState = "Test";
            newBoard.Turn = "BLACK";
            newBoard.WinState = "TBD";
            newBoard.TurnsSinceCapture = 0;

            bm.Add(newBoard);
        }
    }
}
