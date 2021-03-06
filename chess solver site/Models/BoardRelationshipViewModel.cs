using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_solver_site.Models
{
    public class BoardRelationshipViewModel
    {
        private BoardRelationshipModel _model;

        public int Id;
        public int ChildId;
        public int ParentId;

        public BoardRelationshipViewModel()
        {
            _model = new BoardRelationshipModel();
        }
    }
}
