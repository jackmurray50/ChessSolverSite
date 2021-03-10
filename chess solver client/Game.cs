﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace chess_solver_client
{
    /// <summary>
    /// Board represents a board-state
    /// </summary>
    public class ChessBoard
    {
        /// <summary>
        /// the boards Id, used for retrieval and storing child/parent relationships
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The board; this allows one to do board.board[x][y] and get the piece at that position
        /// </summary>
        public List<List<Piece>> Board { get; set; }

        /// <summary>
        /// The pieces currently on the board. Mostly used to trade memory for execution time
        /// </summary>
        public List<Piece> Pieces {get; set;}
        public Colour Turn { get; set; }

        //Used to track the 50-turn rule
        public int TurnsSinceCapture { get; set; }
    
        public ChessBoard(int id, List<Piece> pieces)
        {
            Id = id;
            Pieces = pieces;
            Turn = Colour.WHITE;
            Board = new List<List<Piece>>();

            //Set the board to null
            for(int x = 0; x < 8; x++)
            {
                Board.Add(new List<Piece>());
                for(int y = 0; y < 8; y++)
                {
                    Board[x].Add(null);
                }
            }

            foreach(Piece p in Pieces)
            {
                int x = p.Position.Item1;
                int y = p.Position.Item2;

                p.Board = this;

                Board[x][y] = p;
            }
        }
        /// <summary>
        /// Move a piece
        /// </summary>
        /// <returns> 0 if the game continues, 1 if black wins, 2 if white wins, 3 if its a draw </returns>
        public int Move(Move m)
        {
            int ToReturn = 0;
            //Note: No validation of moves. It's assumed that the validation will take place
            //at move creation
            //Step 1: Check if its been 50 turns since the last capture
            if(TurnsSinceCapture > 49)
            {
                //1.1: If so, return that it was a draw
                return 3;
            }
            //Step 2: Check if there's a piece in the moves destination
            Piece RemovedPiece = Board[m.To.Item1][m.To.Item2];
            if (!(RemovedPiece is null))
            {
                //2.1 If there is, check if its a king. 
                //Again, no verification; we're assuming it happens earlier.
                if(RemovedPiece is King)
                {
                    ToReturn = (int)Turn;
                }
                //2.2 Remove the removed piece from the list of pieces
                Pieces.Remove(RemovedPiece);
            }
            //Step 3:    Hold the piece that's being moved in memory         
            Piece MovedPiece = Board[m.From.Item1][m.From.Item2];
            //Step 4: Move the piece
            Board[m.To.Item1][m.To.Item2] = MovedPiece;
            //4.1: Ensure that the MovedPiece's position is the new position
            MovedPiece.Position = m.To;
            //Step 5: Remove the old piece
            Board[m.From.Item1][m.From.Item2] = null;
            //Step 6: Increment TurnSinceCapture
            TurnsSinceCapture++;
            //Step 7: Return the value
            return ToReturn;
        }

        public override string ToString()
        {
            return base.ToString();
        }
        public string ToString(Move move)
        {
            string output = "  12345678\n-----------";
            for (int x = 0; x < Board.Count; x++)
            {
                output += "\n" + (char)(x + 65) + "|";
                for (int y = 0; y < Board[x].Count; y++)
                {
                    //No piece
                    if (!(Board[x][y] is null))
                    {
                        output += Board[x][y].ConsoleGraphic;
                    }
                    else
                    {
                        //Figure out if a square is white or black
                        if ((x + y) % 2 == 0)
                        {
                            output += 'X';
                        }
                        else
                        {
                            output += '#';
                        }
                    }
                }
                output += "|";
            }
            output += "\n-----------\n";
            return output;
        }

        public List<Piece> this[int key]
        {
            get => Board[key];
            set => Board[key] = value;
        }
    }
    
    public class Move
    {
        public (int, int) To { get; set; }
        public (int, int) From { get; set; }
        public int BoardId { get; set; }
        public Move((int, int) to, (int,int) from, int boardId)
        {
            To = to;
            From = from;
            BoardId = boardId;
        }
    }
    public enum Colour : int
    {
        BLACK = 1,
        WHITE = 2
    }

     
}