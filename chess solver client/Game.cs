using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver_client
{
    /// <summary>
    /// Board represents a board-state
    /// </summary>
    class Board
    {
        /// <summary>
        /// the boards Id, used for retrieval and storing child/parent relationships
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// The board; this allows one to do board.board[x][y] and get the piece at that position
        /// </summary>
        public List<List<Piece>> board { get; set; }

        /// <summary>
        /// The pieces currently on the board. Mostly used to trade memory for execution time
        /// </summary>
        public List<Piece> pieces {get; set;}
        public Colour turn { get; set; }

        //Used to track the 50-turn rule
        public int TurnsSinceCapture { get; set; }
    
        /// <summary>
        /// Move a piece
        /// </summary>
        /// <returns> 0 if the game continues, 1 if black wins, 2 if white wins, 3 if its a draw </returns>
        public int move(Move m)
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
            Piece RemovedPiece = board[m.to.Item1][m.to.Item2];
            if (!(RemovedPiece is null))
            {
                //2.1 If there is, check if its a king. 
                //Again, no verification; we're assuming it happens earlier.
                if(RemovedPiece.type == PieceType.KING)
                {
                    ToReturn = (int)turn;
                }
                //2.2 Remove the removed piece from the list of pieces
                pieces.Remove(RemovedPiece);
            }
            //Step 3:    Hold the piece that's being moved in memory         
            Piece MovedPiece = board[m.from.Item1][m.from.Item2];
            //Step 4: Move the piece
            board[m.to.Item1][m.to.Item2] = MovedPiece;
            //4.1: Ensure that the MovedPiece's position is the new position
            MovedPiece.position = m.to;
            //Step 5: Remove the old piece
            board[m.from.Item1][m.from.Item2] = null;
            //Step 6: Increment TurnSinceCapture
            TurnsSinceCapture++;
            //Step 7: Return the value
            return ToReturn;
        }
    }

    class Piece
    {
        public PieceType type {get; set;}
        public Colour colour {get; set;}

        public (int, int) position { get; set; }
    }
    
    public class Move
    {
        public (int, int) to { get; set; }
        public (int, int) from { get; set; }
        public int boardId { get; set; }
    }
    /// <summary>
    /// The piece type, used to determine its moveset. Note that the Knight is called Hussar
    /// to make sure that each piece has an individual first initial. If it was Knight, it would
    /// conflict with King
    /// </summary>
    public enum PieceType
    {
        PAWN,
        ROOK,
        HUSSAR,
        BISHOP,
        QUEEN,
        KING
    }
    public enum Colour
    {
        BLACK = 1,
        WHITE = 2
    }

     
}
