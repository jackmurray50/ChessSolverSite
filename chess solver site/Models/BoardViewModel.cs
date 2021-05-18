using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace chess_solver_site.Models
{
    public class BoardViewModel
    {
        private BoardModel _model;

        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("BoardState")]
        public string BoardState { get; set; }
        [JsonProperty("TurnsSinceCapture")]
        public int TurnsSinceCapture { get; set; }
        [JsonProperty("Turn")]
        public string Turn { get; set; }
        [JsonProperty("WinState")]
        public string WinState { get; set; }
        [JsonProperty("VerificationAmount")]
        public int VerificationAmount { get; set; }
        [JsonProperty("IsFinished")]
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
                Boards board = _model.GetByBoardState(BoardState, Turn);
                if(board is null)
                {
                    BoardState = "not found";
                }
                else
                {
                    MapProperties(board);

                }
            }
            catch (NullReferenceException)
            {
                BoardState = "not found";
            }
            catch (Exception ex)
            {
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
        
        /// <summary>
        /// The core of the server's input system. Checks if there's an existing record with an identical BoardState AND turn
        /// If so, simply update the TurnsSinceCapture and return the Id.
        /// Otherwise, create a new entry and return its Id.
        /// </summary>
        /// <returns></returns>
        public int AddByBoardState()
        {
            try
            {
                BoardViewModel temp = new BoardViewModel();
                temp.BoardState = BoardState;
                temp.Turn = Turn;
                temp.GetByBoardState();
                if(temp.BoardState == "not found")
                {
                    this.Add();
                }
                else
                {
                    if(this.TurnsSinceCapture < temp.TurnsSinceCapture ||
                        this.IsFinished != temp.IsFinished)
                    {
                        if(TurnsSinceCapture > temp.TurnsSinceCapture)
                        {
                            TurnsSinceCapture = temp.TurnsSinceCapture;
                        }
                        this.IsFinished = true;
                        this.Id = temp.Id;
                        this.Update();
                    }
                    
                    Id = temp.Id;
                }
                
                return Id;
            }
            catch (Exception ex)
            {
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
                board.IsFinished = IsFinished;
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
                Boards board = new Boards();
                board.Id = Id;
                board.BoardState = BoardState;
                board.Turn = Turn;
                board.TurnsSinceCapture = TurnsSinceCapture;
                board.WinState = WinState;
                board.IsFinished = IsFinished;
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
            IsFinished = board.IsFinished;
        }
    }
}
