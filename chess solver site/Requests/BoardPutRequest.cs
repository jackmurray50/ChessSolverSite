using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chess_solver_site.Models;

namespace chess_solver_site.Requests
{
    public class BoardPutRequest
    {
        public List<BoardViewModel> Boards {get; set;}
        public List<BoardRelationshipViewModel> Relationships { get; set; }
    }
}
