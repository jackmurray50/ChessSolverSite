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
        public char ConsoleGraphic { get; set; }

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

    public abstract class LinePiece : Piece
    {
        public LinePiece((int, int) position, Colour colour)
            : base(position, colour) { }

        protected List<Move> GetMovesInLine((int,int) direction)
        {
            List<Move> output = new List<Move>();
            int x = Position.Item1;
            int y = Position.Item2;
            for(int i = 1; true; i++)
            {
                //Check if the new position is out of bounds
                if(ValidatePosition((x + direction.Item1*i, y + direction.Item2 * i)))
                {
                    //Check if its an empty space
                    if(Board[x+direction.Item1*i][y+direction.Item2*i] is null)
                    {
                        output.Add(new Move((x+direction.Item1*i,y+direction.Item2*i), (x,y), Board.Id));
                    }
                    else
                    {
                        //check if its the same colour, but also break; you must stop.  
                        if(Board[x + direction.Item1 * i][y + direction.Item2 * i].Colour != this.Colour)
                        {
                            output.Add(new Move((x+direction.Item1*i,y+direction.Item2*i), (x,y), Board.Id));
                        }
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return output;
        }
    }

    public class Pawn : Piece
    {
        public Pawn((int, int) position, Colour colour)
            : base(position, colour)
        {
            if (colour == Colour.BLACK)
            {
                ConsoleGraphic = 'P';
            }
            else
            {
                ConsoleGraphic = 'p';
            }
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
            if (ValidatePosition((x+(1*compass), y)))
            {
                if (Board[x + (1 * compass)][y] is null)
                {
                    output.Add(new Move((x+ (1 * compass), y ), (x, y), Board.Id));
                    //Fowards 2 spaces
                    if(ValidatePosition((x+(2*compass), y)))
                    {
                        if (Board[x + (2 * compass)][y] is null &&
                            ((x == 1 && this.Colour == Colour.BLACK)
                            || (x == 6 && this.Colour == Colour.WHITE)))
                        {
                            output.Add(new Move((x + (2 * compass), y), (x, y), Board.Id));
                        }
                    }
                }
            }

            //Attacks. Forward 1 space, left/right 1
            //Check that forward 1 + left one is valid
            if(ValidatePosition((x + (1 * compass), y +1))){
                //Check that the space is taken
                if(!(Board[x + (1 * compass)][y+1] is null))
                {
                    //Check that the piece is of a different colour
                    if(Board[x + (1 * compass)][y+1].Colour != Colour)
                    {
                        //Add the value
                        output.Add(new Move(
                            (x + (1 * compass), y+1),
                            (x,y),
                            Board.Id
                            ));
                    }
                }
            }    
            //Attacks. Forward 1 space, left/right 1
            //Check that forward 1 + left one is valid
            if(ValidatePosition((x + (1 * compass), y -1))){
                //Check that the space is taken
                if(!(Board[x + (1 * compass)][y-1] is null))
                {
                    //Check that the piece is of a different colour
                    if(Board[x + (1 * compass)][y-1].Colour != Colour)
                    {
                        //Add the value
                        output.Add(new Move(
                            (x + (1 * compass), y-1),
                            (x,y),
                            Board.Id
                            ));
                    }
                }
            }            

            return output;
        }
    }
    public class Rook : LinePiece
    {
        public Rook((int, int) position, Colour colour)
            : base(position, colour)
        {
            if (colour == Colour.BLACK)
            {
                ConsoleGraphic = 'R';
            }
            else
            {
                ConsoleGraphic = 'r';
            }
        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();
            List<(int, int)> vectors = new List<(int, int)>()
            {
                (1,0),
                (-1,0),
                (-1,0),
                (0,-1)
            };
            foreach ((int, int) v in vectors)
            {
                output.AddRange(GetMovesInLine(v));
            }
            return output;
        }
    }
    public class Hussar : Piece
    {
        public Hussar((int, int) position, Colour colour)
            : base(position, colour)
        {
            if (colour == Colour.BLACK)
            {
                ConsoleGraphic = 'H';
            }
            else
            {
                ConsoleGraphic = 'h';
            }
        }
        public override List<Move> PossibleMoves()
        {
            int x = Position.Item1;
            int y = Position.Item2;
            List<Move> output = new List<Move>();
            //The possible vectors the Rook can take
            List<(int, int)> vectors = new List<(int, int)>()
            {
                (2,1),
                (2,-1),
                (-2,1),
                (-2,-1),
                (1,2),
                (1,-2),
                (-1,2),
                (-1,-2)
            };

            foreach(var v in vectors)
            {
                //Check that each position is within the board
                if(ValidatePosition((x+v.Item1, y + v.Item2))){
                    //Check that the position consiste of either an empty space or
                    //an enemy piece
                    if(Board[x+v.Item1][y+v.Item2] is null)
                    {
                        output.Add(new Move((x + v.Item1, y + v.Item2), (x, y), Board.Id));
                    }
                    else if(Board[x + v.Item1][y + v.Item2].Colour != Colour)
                    {
                        output.Add(new Move((x + v.Item1, y + v.Item2), (x, y), Board.Id));
                    }
                }
            }
            return output;
        }
    }
    public class Bishop : LinePiece
    {
        public Bishop((int, int) position, Colour colour)
            : base(position, colour)
        {
            if (colour == Colour.BLACK)
            {
                ConsoleGraphic = 'B';
            }
            else
            {
                ConsoleGraphic = 'b';
            }
        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();
            List<(int, int)> vectors = new List<(int, int)>()
            {
                (1,1),
                (1,-1),
                (-1,1),
                (-1,1),
            };
            foreach ((int, int) v in vectors)
            {
                output.AddRange(GetMovesInLine(v));
            }
            return output;
        }
    }
    public class Queen : LinePiece
    {
        public Queen((int, int) position, Colour colour)
            : base(position, colour)
        {
            if (colour == Colour.BLACK)
            {
                ConsoleGraphic = 'Q';
            }
            else
            {
                ConsoleGraphic = 'q';
            }
        }
        public override List<Move> PossibleMoves()
        {
            List<Move> output = new List<Move>();
            List<(int, int)> vectors = new List<(int, int)>()
            {
                (1,1),
                (1,0),
                (1,-1),
                (-1,1),
                (-1,0),
                (-1,1),
                (0,1),
                (0,-1)
            };
            foreach((int,int) v in vectors)
            {
                output.AddRange(GetMovesInLine(v));
            }
            return output;
        }
    }
    public class King: Piece
    {
        public King((int, int) position, Colour colour)
            : base(position, colour)
        {
            if (colour == Colour.BLACK)
            {
                ConsoleGraphic = 'K';
            }
            else
            {
                ConsoleGraphic = 'k';
            }
        }
        public override List<Move> PossibleMoves()
        {
            int x = Position.Item1;
            int y = Position.Item2;
            List<Move> output = new List<Move>();
            //The possible vectors the Rook can take
            List<(int, int)> vectors = new List<(int, int)>()
            {
                (1,1),
                (1,0),
                (1,-1),
                (-1,1),
                (-1,0),
                (-1,1),
                (0,1),
                (0,-1)
            };

            foreach (var v in vectors)
            {
                //Check that each position is within the board
                if (ValidatePosition((x + v.Item1, y + v.Item2)))
                {
                    //Check that the position consiste of either an empty space or
                    //an enemy piece
                    if (Board[x + v.Item1][y + v.Item2] is null)
                    {
                        output.Add(new Move((x + v.Item1, y + v.Item2), (x, y), Board.Id));
                    }
                    else if (Board[x + v.Item1][y + v.Item2].Colour != Colour)
                    {
                        output.Add(new Move((x + v.Item1, y + v.Item2), (x, y), Board.Id));
                    }
                }
            }
            return output;
        }
    }
}

