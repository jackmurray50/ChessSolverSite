using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_solver_site.Models
{
    public class Boards : ChessSolverEntity
    {
        public string BoardState { get; set; }
        public int TurnsSinceCapture { get; set; }
        public string Turn { get; set; }
        public string WinState { get; set; }
        public int VerificationAmount { get; set; }
        public bool IsFinished { get; set; }
    }

    public class BoardsRelationships : ChessSolverEntity
    {
        public int ChildId { get; set; }
        public int ParentId { get; set; }
    }
}
