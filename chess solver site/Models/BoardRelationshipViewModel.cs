using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_solver_site.Models
{
    public class BoardRelationshipViewModel
    {
        private BoardRelationshipModel _model;

        public int Id { get; set; }
        public int ChildId { get; set; }
        public int ParentId { get; set; }

        public BoardRelationshipViewModel()
        {
            _model = new BoardRelationshipModel();
        }
    }
}
