using System;
using Xunit;
using Xunit.Abstractions;
using chess_solver_client;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
namespace ChessSolverClientTests
{
    public class BoardTests
    {
        private readonly ITestOutputHelper output;

        public BoardTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test_ImportConstructor()
        {
            ChessBoard Board = new ChessBoard(0, "RHBQKBHR\nPPPPPPPP\nX#X#X#X#\nX#X#X#X#\nX#X#X#X#\nX#X#X#X#\npppppppp\nrhbqkbhr",
                                             0, "BLACK");
            output.WriteLine(Board.UglyToString());
        }

        //Implicitly tested from the Pieces tests, but im testing this anyway.
        [Fact]
        public void Test_MainConstructor()
        {
            List<Piece> toAdd = new List<Piece>();
            //Pawns
            for(int i = 0; i < 8; i++)
            {
                toAdd.Add(new Pawn((1,i), Colour.BLACK));
            }
            for(int i = 0; i < 8; i++)
            {
                toAdd.Add(new Pawn((6,i), Colour.WHITE));
            }
            toAdd.Add(new Rook((7,0), Colour.WHITE));
            toAdd.Add(new Rook((7,7), Colour.WHITE));
            toAdd.Add(new Hussar((7,1), Colour.WHITE));
            toAdd.Add(new Hussar((7,6), Colour.WHITE));
            toAdd.Add(new Bishop((7,2), Colour.WHITE));
            toAdd.Add(new Bishop((7,5), Colour.WHITE));
            toAdd.Add(new King((7, 4), Colour.WHITE));
            toAdd.Add(new Queen((7,3), Colour.WHITE));

            toAdd.Add(new Rook((0,0), Colour.BLACK));
            toAdd.Add(new Rook((0,7), Colour.BLACK));
            toAdd.Add(new Hussar((0,1), Colour.BLACK));
            toAdd.Add(new Hussar((0,6), Colour.BLACK));
            toAdd.Add(new Bishop((0,2), Colour.BLACK));
            toAdd.Add(new Bishop((0,5), Colour.BLACK));
            toAdd.Add(new King((0, 4), Colour.BLACK));
            toAdd.Add(new Queen((0,3), Colour.BLACK));
            ChessBoard board = new ChessBoard(0, toAdd);

            output.WriteLine(board.ToString());

        }
    }

}
