using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace chess_solver_site.Models
{
    public class BoardViewModel
    {
        private BoardModel _model;

        public int Id { get; set; }
        public string BoardState { get; set; }
        public int TurnsSinceCapture { get; set; }
        public string Turn { get; set; }
        public string WinState { get; set; }
        public int VerificationAmount { get; set; }
        public bool IsFinished { get; set; }

        public BoardViewModel()
        {
            _model = new BoardModel();
        }


        public void GetById()
        {
            try
            {
                Boards board = _model.GetById(Id);

                MapProperties(board);
            }
            catch (NullReferenceException)
            {
                BoardState = "not found";
            }
            catch (Exception ex)
            {
                BoardState = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    
        public void GetByBoardState()
        {
            try
            {
                Boards board = _model.GetByBoardState(BoardState);

                MapProperties(board);
            }
            catch (NullReferenceException)
            {
                BoardState = "not found";
            }
            catch (Exception ex)
            {
                BoardState = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    
        public void GetLeaf()
        {
            try
            {
                Boards board = _model.GetUnfinishedBoard();
                MapProperties(board);

            }
            catch (NullReferenceException)
            {
                BoardState = "not found";
            }
            catch (Exception ex)
            {
                BoardState = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        
        public void GetUnverifiedBranch()
        {
            try
            {
                Boards board = _model.GetBoardByVerificationAmount(3);
                MapProperties(board);
            }
            catch (NullReferenceException)
            {
                BoardState = "not found";
            }
            catch (Exception ex)
            {
                BoardState = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        public void Add()
        {
            try
            {
                Id = -1;
                Boards board = new Boards();
                board.BoardState = BoardState;
                board.Turn = Turn;
                board.TurnsSinceCapture = TurnsSinceCapture;
                board.WinState = WinState;
                Id = _model.Add(board);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    
        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                Id = -1;
                Boards board = new Boards();
                board.BoardState = BoardState;
                board.Turn = Turn;
                board.TurnsSinceCapture = TurnsSinceCapture;
                board.WinState = WinState;
                opStatus = _model.Update(board);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus);
        }

        public int Delete()
        {
            int boardDeleted = -1;

            try
            {
                boardDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return boardDeleted;
        }
    
        private void MapProperties(Boards board)
        {
            Id = board.Id;
            BoardState = board.BoardState;
            Turn = board.Turn;
            TurnsSinceCapture = board.TurnsSinceCapture;
            WinState = board.WinState;
            VerificationAmount = board.VerificationAmount;
        }
    }
}
