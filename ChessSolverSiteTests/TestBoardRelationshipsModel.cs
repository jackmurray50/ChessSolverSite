using System;
using Xunit;
using chess_solver_site.Models;
using chess_solver_site;


namespace ChessSolverSiteTests
{
    public class TestBoardRelationshipsModel
    {
        BoardRelationshipModel brm = new BoardRelationshipModel();

        [Fact]
        public void Test_Add()
        {
            BoardsRelationships br = new BoardsRelationships();
            br.ChildId = 1;
            br.ParentId = 2;

            brm.Add(br);
        }
        [Fact]
        public void Test_Delete()
        {
            Assert.True(brm.Delete(1) == 1);
        }
        [Fact]
        public void Test_GetAllParentsOfId()
        {

        }
        [Fact]
        public void Test_GetAllChildrenOfId()
        {

        }
        [Fact]
        public void Test_GetByParentAndChild()
        {

        }
    }
}
