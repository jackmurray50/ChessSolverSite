using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace chess_solver_client
{
    public abstract class Piece
    {

        public Piece((int, int) position, Colour colour)
        {
            Position = position;
            Colour = colour;
        }
        public ChessBoard Board { get; set; }
        public Colour Colour { get; set; }

        public (int, int) Position { get; set; }

        /// <summary>
        /// PossibleMoves will find all the possible legal moves for a piece. all the verification happens
        /// here, so the rest of the application doesn't need to check.
        /// </summary>
        /// <param name="board">The board this piece belongs to</param>
        /// <returns></returns>
        public abstract List<Move> PossibleMoves();

        public bool ValidatePosition((int, int) position)
        {
            int x = position.Item1;
            int y = position.Item2;

            if(x > 7 ||
                x < 0 ||
                y > 7 ||
                y < 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }

    public class Pawn : Piece
    {
        public Pawn((int, int) position, Colour colour)
            : base(position, colour)
        {

        }

        //Pawns have a few moves, but they're complicated:
        //Forwards 1 space if there's nothing in the way
        //forwards 2 spaces if there's nothing in the way and the pawn hasn't moved yet
        //Left 1 and forwards 1 if there's an enemy in the way
        //Right 1 and forwards 1 if there's an enemy in the way
        //This is a pain because I need to check for each condition
        public override List<Move> PossibleMoves()
        {
            //Pawns care about which way is 'north', and the compass tells them which way
            //to go.
            int compass = 0;
            if(this.Colour == Colour.WHITE)
            {
                compass = -1;
            }
            if(this.Colour == Colour.BLACK)
            {
                compass = 1;
            }
            int x = Position.Item1;
            int y = Position.Item2;

            List<Move> output = new List<Move>();
            //Forwards 1 or 2 space
            //Check that what we're about to test stays inside the board
            if (ValidatePosition((x, y+(1*compass))))
            {
                if (Board[x][y + (1 * compass)] is null)
                {
                    output.Add(new Move((x, y + (1 * compass)), (x, y), Board.Id));
                    //Fowards 2 spaces
                    if(ValidatePosition((x, y+(2*compass))))
                    {
                        if (Board[x][y + (2 * compass)] is null &&
                            ((y == 1 && this.Colour == Colour.BLACK)
                            || (y == 6 && this.Colour == Colour.WHITE)))
                        {
                            output.Add(new Move((x, y + (2 * compass)), (x, y), Board.Id));
                        }
                    }
                }
            } 

            //Attacks. Forward 1 space, up/down 1
            

            return output;
        }
    }
    public class Rook : Piece
    {
        public Rook((int, int) position, Colour colour)
            : base(position, colour)
        {

        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class Hussar : Piece
    {
        public Hussar((int, int) position, Colour colour)
            : base(position, colour)
        {

        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class Bishop : Piece
    {
        public Bishop((int, int) position, Colour colour)
            : base(position, colour)
        {

        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class Queen : Piece
    {
        public Queen((int, int) position, Colour colour)
            : base(position, colour)
        {

        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
    public class King : Piece
    {
        public King((int, int) position, Colour colour)
            : base(position, colour)
        { 

        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();

            return output;
        }
    }
}

