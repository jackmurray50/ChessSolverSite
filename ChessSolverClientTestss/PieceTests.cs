using System;
using Xunit;
using Xunit.Abstractions;
using chess_solver_client;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ChessSolverClientTestss
{
    public class PieceTests
    {
        private readonly ITestOutputHelper output;

        public PieceTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void Test_PawnPossibleMoves()
        {
            Pawn pawn = new Pawn((4, 1), Colour.BLACK);
            Rook ToDieRook = new Rook((5, 2), Colour.WHITE);
            Rook ToLiveRook = new Rook((3, 2), Colour.BLACK);
            ChessBoard board = new ChessBoard(0, new List<Piece>() { 
                pawn, ToDieRook, ToLiveRook
            });

            List<Move> moves = pawn.PossibleMoves();
            //Linq query to grab all (Hopefully just 1) Move with the right parameters
            foreach(var m in moves)
            {
                output.WriteLine(m.To + ", " + m.From);
            }
            
            int ForwardsMatches = (from m in moves where
                           (m.To == (4, 2) && m.From == (4, 1)) select m).Count();
            int ForwardsTwoMatches = (from m in moves
                                   where
           (m.To == (4, 3) && m.From == (4, 1))
                                   select m).Count();
            int AttackRight = (from m in moves
                                   where
           (m.To == (5, 2) && m.From == (4, 1))
                                   select m).Count();
            int AttackLeft = (from m in moves
                                   where
           (m.To == (3, 2) && m.From == (4, 1))
                                   select m).Count();
            Assert.True(ForwardsMatches == 1 && ForwardsTwoMatches == 1 && AttackRight == 1 && AttackLeft == 0);

        }
    }
}
