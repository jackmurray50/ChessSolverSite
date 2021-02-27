using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace chess_solver_site.Models
{
    public class ChessSolverEntity
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] Timer { get; set; }
    }
}
