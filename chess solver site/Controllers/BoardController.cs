using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet("GetLeaf")]
        public ActionResult GetLeaf()
        {
            //1. Ask the DB to supply a random leaf
            try
            {
                BoardViewModel vm = new BoardViewModel();
                vm.GetLeaf();
                return Ok(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetUnverifiedBranch")]
        public ActionResult GetUnverifiedBranch()
        {
            try
            {
                BoardViewModel vm = new BoardViewModel();
                vm.GetUnverifiedBranch();
                return Ok(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    
        [HttpPost]
        public ActionResult Put(BoardViewModel board)
        {
            try
            {
                board.Add();
                return Ok("Board added with Id " + board.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
