using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

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
    
        public int Add()
        {
            try
            {
                Id = -1;
                BoardsRelationships bvm = new BoardsRelationships();
                bvm.Id = Id;
                bvm.ChildId = ChildId;
                bvm.ParentId = ParentId;
                Id = _model.Add(bvm);
                return Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
    }
}
