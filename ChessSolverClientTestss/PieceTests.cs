using System;
using Xunit;
using chess_solver_client;
using System.Collections;
using System.Collections.Generic;
namespace ChessSolverClientTestss
{
    public class PieceTests
    {
        [Fact]
        public void Test_PawnPossibleMoves()
        {
            Pawn pawn = new Pawn((1, 4), Colour.BLACK);
            ChessBoard board = new ChessBoard(0, new List<Piece>() { 
                pawn
            });

            List<Move> moves = pawn.PossibleMoves();

        }
    }
}
