using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver_client
{
    public abstract class Piece
    {

        public Piece((int, int) position, PieceType type, Colour colour, ChessBoard board)
        {
            Position = position;
            Type = type;
            Colour = colour;
            Board = board;
        }
        public ChessBoard Board { get; set; }
        public PieceType Type { get; set; }
        public Colour Colour { get; set; }

        public (int, int) Position { get; set; }

        /// <summary>
        /// PossibleMoves will find all the possible legal moves for a piece. all the verification happens
        /// here, so the rest of the application doesn't need to check.
        /// </summary>
        /// <param name="board">The board this piece belongs to</param>
        /// <returns></returns>
        public abstract List<Move> PossibleMoves(ChessBoard board);
    }

    public class Pawn : Piece
    {
        public Pawn((int, int) position, PieceType type, Colour colour, ChessBoard board)
            : base(position, type, colour, board)
        {

        }
        public override List<Move> PossibleMoves(ChessBoard board)
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class Rook : Piece
    {
        public Rook((int, int) position, PieceType type, Colour colour, ChessBoard board) 
            : base(position, type, colour, board)
        {

        }
        public override List<Move> PossibleMoves(ChessBoard board)
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class Hussar : Piece
    {
        public Hussar((int, int) position, PieceType type, Colour colour, ChessBoard board)
            : base(position, type, colour, board)
        {

        }
        public override List<Move> PossibleMoves(ChessBoard board)
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class Bishop : Piece
    {
        public Bishop((int, int) position, PieceType type, Colour colour, ChessBoard board)
            : base(position, type, colour, board)
        {

        }
        public override List<Move> PossibleMoves(ChessBoard board)
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class Queen : Piece
    {
        public Queen((int, int) position, PieceType type, Colour colour, ChessBoard board)
            : base(position, type, colour, board)
        {

        }
        public override List<Move> PossibleMoves(ChessBoard board)
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class King : Piece
    {
        public King((int, int) position, PieceType type, Colour colour, ChessBoard board)
            : base(position, type, colour, board) 
        { 

        }
        public override List<Move> PossibleMoves(ChessBoard board)
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
}

