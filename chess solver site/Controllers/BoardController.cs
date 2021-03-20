﻿using Microsoft.AspNetCore.Http;
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
        public ActionResult Put((List<BoardViewModel> boards, List<BoardRelationshipViewModel> relationships) payload)
        {
            try
            {
                Dictionary<int, int> IdDictionary = new Dictionary<int, int>();
                //Go through the boards, adding them BY BOARD STATE, not by Id. This enforces memoization
                foreach(BoardViewModel bvm in payload.boards)
                {
                    int OriginalId = bvm.Id;
                    int newId = bvm.AddByState();
                    IdDictionary.Add(OriginalId, newId);
                }
                //Each time one is added, get its Id. Add it to a dictionary, in the form of OriginalId:CurrentId
                //After all boards are added, add the relationships after modifying them using the Id Dictionary
                foreach(BoardRelationshipViewModel brvm in payload.relationships)
                {
                    brvm.ChildId = IdDictionary[brvm.ChildId];
                    brvm.ParentId = IdDictionary[brvm.ParentId];
                    brvm.Add();
                }
                return Ok($"{50} boards added");
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
