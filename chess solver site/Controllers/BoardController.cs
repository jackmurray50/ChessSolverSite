using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using chess_solver_site.Models;
using System.Text.Json;
using System.Diagnostics;

namespace chess_solver_site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ILogger _logger;
        public BoardController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets a leaf of the gametree. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetLeaf()
        {
            //1. Ask the DB to supply a random leaf
            try
            {
                BoardViewModel vm = new BoardViewModel();
                vm.G
                vm.WinState = "TBD";
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult GetUnverifiedBranch()
        {

        }
    }
}
