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
    
    
        [Fact]
        public void Test_HussarPossibleMoves()
        {
            Hussar Hussar = new Hussar((4, 4), Colour.BLACK);
            Pawn ToLive = new Pawn((6, 5), Colour.BLACK);
            Pawn ToDie = new Pawn((2, 3), Colour.WHITE);
            ChessBoard board = new ChessBoard(0, new List<Piece>() {Hussar,
                ToDie, ToLive
            });

            List<Move> moves = Hussar.PossibleMoves();
            foreach (var m in moves)
            {
                output.WriteLine(m.To + ", " + m.From);
            }
            //Check that there's at least one good move.
            //Will manually check the other moves for now, but automatic testing is
            //on the todo list.
            Assert.True(moves.Count() == 7);

        }
    
        [Fact]
        public void Test_KingPossibleMoves()
        {
            King King = new King((4, 4), Colour.BLACK);
            Pawn ToLive = new Pawn((4, 5), Colour.BLACK);
            Pawn ToDie = new Pawn((3, 3), Colour.WHITE);
            ChessBoard board = new ChessBoard(0, new List<Piece>() {King,
                ToDie, ToLive
            });

            List<Move> moves = King.PossibleMoves();
            foreach (var m in moves)
            {
                output.WriteLine(m.To + ", " + m.From);
            }
            //Check that there's at least one good move.
            //Will manually check the other moves for now, but automatic testing is
            //on the todo list.
            Assert.True(moves.Count() == 7);
        }
    }
}
