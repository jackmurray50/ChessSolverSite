using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_solver_site.Models
{
    public class Accounts : ChessSolverEntity
    {

        public string Password;
        public string Name;
        //TODO: May need to change data type in the future
        public int Progress;
    }
}
