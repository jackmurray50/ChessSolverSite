using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver_client
{
    class BoardViewModel
    {
        public int Id { get; set; }
        public string BoardState { get; set; }
        public int TurnsSinceCapture { get; set; }
        public string Turn { get; set; }
        public string WinState { get; set; }
        public int VerificationAmount { get; set; }
    }
}
