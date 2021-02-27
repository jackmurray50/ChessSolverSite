using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver_site
{
    public enum UpdateStatus
    {
        Ok = 1,
        Failed = -1,
        Stale = -2
    }
}