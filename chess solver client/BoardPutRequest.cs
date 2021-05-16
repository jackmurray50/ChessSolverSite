using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver_client
{
    public class BoardPutRequest
    {
        public List<BoardViewModel> Boards { get; set; }
        public List<BoardRelationshipViewModel> Relationships { get; set; }
    }
}
