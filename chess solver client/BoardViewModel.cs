using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver_client
{
    public class BoardViewModel
    {
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
    }
}
