using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver_client
{
    public class BoardRelationshipViewModel
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public int ParentId { get; set; }

        public BoardRelationshipViewModel (int id, int childId, int parentId)
        {
            Id = id;
            ChildId = childId;
            ParentId = parentId;
        }
    }
}
